namespace nChain.Common
{
    public class Transaction
    {
        /// <summary>
        /// Coin gönderen
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// Coin alan kişi
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// Gönderilen Coin sayısı
        /// </summary>
        public float Value { get; set; }

        public override string ToString()
        {
            return Sender + ":" + Receiver + ":" + Value;
        }
    }
}
