using System.Collections.Generic;
using System.Threading.Tasks;
using Tatum.CSharp.Core.Model;
using Tatum.CSharp.Ethereum.Clients;

namespace Tatum.CSharp.Demo.ExampleServices.Ethereum;

public class BlockchainTransferExampleService
{
    private readonly IEthereumClient _ethereumClient;

    public BlockchainTransferExampleService(IEthereumClient ethereumClient)
    {
        _ethereumClient = ethereumClient;
    }

    private Dictionary<string, string> _someInternalPersistence = new Dictionary<string, string>()
    {
        { "address1", "privateKey1" },
        { "address2", "privateKey2" },
        { "address3", "privateKey3" }
    };
    
    public async Task<TransactionHash> BlockchainTransfer(string fromAddress, string toAddress, string amount)
    {
        // Need to know the private key of the address that is sending the amount.
        // In this example, we are using a dictionary to store the private keys.
        // In a real world scenario, you would store the private keys in a secure location.
        var fromPrivKey = _someInternalPersistence[fromAddress];

        var transfer = new TransferEthBlockchain(
            null,
            0,
            toAddress, // address you would like to send to
            TransferEthBlockchain.CurrencyEnum.ETH,
            null,
            amount, // amount you would like to send eg. "0.00001"
            fromPrivKey);


        TransactionHash transactionHash = await _ethereumClient.EthereumBlockchain.EthBlockchainTransferAsync(transfer);
        
        return transactionHash;
    }
}