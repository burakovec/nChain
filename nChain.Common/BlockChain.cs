using nChain.Common.Models;
using Serilog;

namespace nChain.Common
{
    public class BlockChain
    {
        /// <summary>
        /// Blockchain içerisinde bulunan bloklarımız.
        /// </summary>
        public List<Block> Chain { get; set; }
        /// <summary>
        /// Blok'un doğru Hash değerine sahip olması için hash içerisinde bulunması gereken 0-'sıfır' sayısı
        /// </summary>
        private int Difficulty;
        public BlockChain(Block genesisBlock)
        {
            Difficulty = 4;
            Chain = new List<Block>();
            Chain.Add(genesisBlock);
        }
        /// <summary>
        /// Blok'a ait HASH değerini oluşturmak için kullanılan metodtur
        /// </summary>
        /// <param name="blockToGenerate">Hash'i oluşturulacak blok</param>
        /// <returns></returns>
        private string GenerateHash(Block blockToGenerate)
        {
            SHA256Manager sHA256 = new SHA256Manager();
            return sHA256.GetHash(blockToGenerate.ToString());
        }
        /// <summary>
        /// Blok oluşturma ve oluşturulan bloku zincire ekleyen metod
        /// </summary>
        public void Mine(Block blockToAdd)
        {
            blockToAdd.PrevHash = Chain[Chain.Count - 1].Hash;
            while (true)
            {
                blockToAdd.Nonce++;
                blockToAdd.Hash = GenerateHash(blockToAdd);
                Log.Information("Block Hash : {Hash} Block Nonce : {Nonce} Block Id : {UUID}", blockToAdd.Hash, blockToAdd.Nonce, blockToAdd.UUID);
                if (IsValid(blockToAdd))
                {
                    break;
                }
            }
            Chain.Add(blockToAdd);
        }
        /// <summary>
        /// Bu metod eklenen blokun hash değerinin istenen zorlukta olup olmadığını kontrol eder
        /// </summary>
        /// <param name="blockToAdd">Hash değeri kontrol edilecek olan blok</param>
        /// <returns></returns>
        private bool IsValid(Block blockToAdd)
        {
            string Zeros = "000000000000000000000000000000000000000000";
            return blockToAdd.Hash.StartsWith(Zeros.Substring(0, Difficulty));
        }
        /// <summary>
        /// Geçerli bloktan önceki blokun geçerli olup olmadığını kontrol eden metod
        /// </summary>
        /// <param name="currentBlock">Geçerli blok</param>
        /// <param name="prevBlock">Önceki blok</param>
        /// <returns></returns>
        private bool IsValidPreviousBlock(Block currentBlock, Block prevBlock)
        {
            var prevIsValid = IsValid(prevBlock);
            var hashIsCorrect = currentBlock.PrevHash == prevBlock.Hash;
            var currentIsValid = IsValid(currentBlock);
            return prevIsValid && hashIsCorrect && currentIsValid;

        }
        /// <summary>
        /// Tüm Zincir'in geçerli olup olmadığını kontrol eden metod
        /// </summary>
        /// <param name="blockChain">Kontrol edilecek Blok Zinciri</param>
        /// <returns></returns>
        private bool IsValidChain(List<Block> blockChain)
        {
            if (blockChain.Count < 2)
                return true;
            for (int i = 1; i < blockChain.Count; i += 2)
            {
                if (!IsValidPreviousBlock(blockChain[i + 1], blockChain[i]))
                    return false;
            }
            if (!IsValidPreviousBlock(blockChain[blockChain.Count - 1], blockChain[blockChain.Count - 2]))
                return false;
            return true;
        }
        /// <summary>
        /// Zincir içerisindeki bloklarda meydana gelen çakışmaları çözecek olan metod.
        /// </summary>
        /// <returns></returns>
        private bool ResolveConflicts()
        {
            return true;
        }
    }
}
