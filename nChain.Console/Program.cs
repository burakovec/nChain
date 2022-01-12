using nChain.Common;
using nChain.Common.Models;
using Serilog;
using Serilog.Events;
//
Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .WriteTo.Seq("http://localhost:5341", apiKey: "TdxpfQuRXgxxaSA1tSxF")
              .CreateLogger();
Log.Information("nChain Application Started.");
BlockChain blockChain = new BlockChain(new Block()
{
    Data = new List<Transaction>(),
    Hash = "00000000000000000000000000000000000000000000000000000000",
    Nonce = 1,
    PrevHash = "00000000000000000000000000000000000000000000000000000",
    TimeStamp = DateTime.UtcNow
});
int i = 1;
while (true)
{
    i = i * 3;
    List<Transaction> lstT = new List<Transaction>();
    lstT.Add(new Transaction()
    {
        Receiver = "7893hf813gf21gf21r8gf8f823r8f9t2367ft6236tf7",
        Sender = "d7823h823fgh2636f238fg2683f2367yf238f62368f2t3f68236tf",
        Value = i
    });
    Block b = new Block()
    {
        Data = lstT,
        TimeStamp = DateTime.UtcNow
    };
    blockChain.Mine(b);
};