namespace EshopMicro6.Payments.PaymentProcessor;

public class ProcessPayment : IProcessPayment
{
    public bool PaymentProcessor()
    {
        //Normall there will be a payment process which would check the card details 
        //However for the sake of the project which is focused on Microservices architecture
        //The payment process will just return true
        return true;
    }
}
