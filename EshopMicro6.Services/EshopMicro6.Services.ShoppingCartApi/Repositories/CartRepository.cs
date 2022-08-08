using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EshopMicro6.Services.ShoppingCartApi.Data;
using EshopMicro6.Services.ShoppingCartApi.DTOs;
using EshopMicro6.Services.ShoppingCartApi.Entities;
using EshopMicro6.Services.ShoppingCartApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EshopMicro6.Services.ShoppingCartApi.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _dbContext; 
    private IMapper _mapper;

    public CartRepository(AppDbContext dbContext, IMapper mapper) 
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<bool> ApplyCoupon(string userID, string couponCode)
    {
        var cartFromDB = await _dbContext.CartHeaders.FirstOrDefaultAsync(u => u.UserID == userID);
        cartFromDB.CouponCode = couponCode;
        _dbContext.CartHeaders.Update(cartFromDB);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ClearShoppingCart(string UserID)
    {
        var cartHeaderFromDb = await _dbContext.CartHeaders.FirstOrDefaultAsync(u => u.UserID == UserID); 

        if (cartHeaderFromDb != null) 
        {
            _dbContext.CartDetails.RemoveRange(_dbContext.CartDetails.Where(u => u.CartHeaderID == cartHeaderFromDb.CartHeaderID));
            _dbContext.CartHeaders.Remove(cartHeaderFromDb);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<CartDTO> GetCartByUserID(string UserID)
    {
        Cart cart = new() 
        {
            cartHeader = await _dbContext.CartHeaders.FirstOrDefaultAsync(u => u.UserID == UserID)
        };

        cart.CartDetails = _dbContext.CartDetails.Where(u => u.CartHeaderID == cart.cartHeader.CartHeaderID).Include(u => u.Product);

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<bool> RemoveCoupon(string userID)
    {
        var cartFromDB = await _dbContext.CartHeaders.FirstOrDefaultAsync(u => u.UserID == userID);
        cartFromDB.CouponCode = string.Empty;
        _dbContext.CartHeaders.Update(cartFromDB);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveFromCart(int CartDetailsID)
    {
        try 
        {
            CartDetails cartDetails = await _dbContext.CartDetails.FirstOrDefaultAsync(x => x.CartDetailsID == CartDetailsID);

            int cartItems = _dbContext.CartDetails.Where(x => x.CartHeaderID == cartDetails.CartHeaderID).Count();

            _dbContext.CartDetails.Remove(cartDetails);

            if (cartItems == 1)
            {
                var cartHeader = await _dbContext.CartHeaders.FirstOrDefaultAsync(x => x.CartHeaderID == cartDetails.CartHeaderID);

                _dbContext.CartHeaders.Remove(cartHeader);
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch 
        {
            return false;
        }
    }

    public async Task<CartDTO> UpsertCart(CartDTO cartDTO)
    {
        Cart cart = _mapper.Map<Cart>(cartDTO);

        var prodInDb = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == cartDTO.CartDetails.FirstOrDefault().ProductId);

        if (prodInDb == null) 
        {
            await _dbContext.Products.AddAsync(cart.CartDetails.FirstOrDefault().Product);

            await _dbContext.SaveChangesAsync();
        }

        var cartHeaderFromDb = await _dbContext.CartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.UserID == cart.cartHeader.UserID);
        
        if (cartHeaderFromDb == null) 
        {
            await _dbContext.CartHeaders.AddAsync(cart.cartHeader);

            await _dbContext.SaveChangesAsync();

            cart.CartDetails.FirstOrDefault().CartHeaderID = cart.cartHeader.CartHeaderID;
            cart.CartDetails.FirstOrDefault().Product = null;
            _dbContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
            
            await _dbContext.SaveChangesAsync();
        }
        else 
        {
            // if header is not null check if details has same product
            var CartDetailsFromDB = await _dbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId && u.CartHeaderID == cartHeaderFromDb.CartHeaderID);

            if (CartDetailsFromDB == null)
            {
                // create details
                cart.CartDetails.FirstOrDefault().CartHeaderID = cartHeaderFromDb.CartHeaderID;
                cart.CartDetails.FirstOrDefault().Product = null;
                await _dbContext.CartDetails.AddAsync(cart.CartDetails.FirstOrDefault());
                await _dbContext.SaveChangesAsync();
            }
            else 
            {
                // update the count / cart details
                cart.CartDetails.FirstOrDefault().Count += CartDetailsFromDB.Count;
                cart.CartDetails.FirstOrDefault().Product = null;
                _dbContext.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                await _dbContext.SaveChangesAsync();
            }
        }

        return _mapper.Map<CartDTO>(cart);
    }
}
