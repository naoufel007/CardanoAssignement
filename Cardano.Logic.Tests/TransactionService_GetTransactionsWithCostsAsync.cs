using Cardano.Domain.Dto;
using Cardano.Domain.Entity;
using Cardano.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cardano.Logic.Tests
{
    public class TransactionService_GetTransactionsWithCostsAsync
    {
        private ITransactionService _transactionService;
        private Mock<IGleifApiRepository> _gleifApiRepositoryMock;

        public TransactionService_GetTransactionsWithCostsAsync()
        {
            _gleifApiRepositoryMock = new Mock<IGleifApiRepository>();
            _transactionService = new TransactionService(_gleifApiRepositoryMock.Object);
        }

        [Fact]
        public async Task ThrowExceptionIfNoTransactionFoundTestAsync()
        {
            List<TransactionDto> transactionDtos = new List<TransactionDto>();
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _transactionService.GetTransactionsWithCostsAsync(transactionDtos));
            Assert.Equal("No transaction found", ex.Message);
        }

        [Fact]
        public async Task ThrowExceptionIfInvalidLeiDataTestAsync()
        {
            List<TransactionDto> transactionDtos = new List<TransactionDto>() { new TransactionDto() { Uti = "Uti", Rate = 0.2m } };
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _transactionService.GetTransactionsWithCostsAsync(transactionDtos));
            Assert.Equal("Some transactions does not have the Lei column", ex.Message);
        }

        [Fact]
        public async Task ThrowExceptionIfInvalidRateDataTestAsync()
        {
            List<TransactionDto> transactionDtos = new List<TransactionDto>() { new TransactionDto() { Lei = "Lei", Rate = decimal.Zero } };
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _transactionService.GetTransactionsWithCostsAsync(transactionDtos));
            Assert.Equal("Some transactions does not have a valid rate, rate is required and must be greater than zero.", ex.Message);
        }

        [Fact]
        public async Task ThrowExceptionIfInvalidNationalDataTestAsync()
        {
            List<TransactionDto> transactionDtos = new List<TransactionDto>() { new TransactionDto() { Lei = "Lei", Rate = 0.11m, National = -1 } };
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _transactionService.GetTransactionsWithCostsAsync(transactionDtos));
            Assert.Equal("Some transactions does not have a valid National, National is required and must be greater than zero.", ex.Message);
        }

        [Fact]
        public async Task ThrowExceptionIfApiReturnWrongDataTestAsync()
        {
            List<TransactionDto> transactionDtos = new List<TransactionDto>() { new TransactionDto() { Lei = "LeiXX", Rate = 0.11m, National = 1 } };
            Root root = new Root() { Data = new List<Datum> { new Datum() { Id = "YYY" } } };

            _gleifApiRepositoryMock.Setup(api => api.GetDataByLeiAsync("LeiXX")).ReturnsAsync(root);

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _transactionService.GetTransactionsWithCostsAsync(transactionDtos));
            Assert.Equal("The Api did not send data for this Lei : LeiXX", ex.Message);
        }

        [Fact]
        public async Task CalculateCostForGBTestAsync()
        {
            TransactionDto transactionDto = new TransactionDto() { Lei = "LeiXX", Rate = 0.1m, National = 20 };
            List<TransactionDto> transactionDtos = new List<TransactionDto>() { transactionDto };
            Entity entity = new() { LegalName = new() { Name = "legal_name" }, LegalAddress = new() { Country = "GB" } };
            Attributes attributes = new() { Lei = "LeiXX", Bic = new List<string> { "AB", "96KO" }, Entity = entity };
            Root root = new Root() { Data = new List<Datum> { new Datum() { Attributes = attributes } } };

            _gleifApiRepositoryMock.Setup(api => api.GetDataByLeiAsync("LeiXX")).ReturnsAsync(root);

            List<Transaction> transactions = await _transactionService.GetTransactionsWithCostsAsync(transactionDtos);
            Assert.NotNull(transactions);
            Assert.NotEmpty(transactions);
            Assert.Equal(1, transactions.Count);

            Transaction transaction = transactions.First();

            Assert.Equal(transactionDto.Lei, transaction.Lei);
            Assert.Equal(transactionDto.Rate.Value, transaction.Rate);
            Assert.Equal(transactionDto.National.Value, transaction.National);
            Assert.Equal(entity.LegalName.Name, transaction.LegalName);
            Assert.Equal(string.Join("/", attributes.Bic), transaction.Bic);
            Assert.Equal(transactionDto.National.Value * (transactionDto.Rate.Value - 1), transaction.Cost);
        }

        [Fact]
        public async Task CalculateCostForNLTestAsync()
        {
            TransactionDto transactionDto = new TransactionDto() { Lei = "LeiXX", Rate = 0.1m, National = 20 };
            List<TransactionDto> transactionDtos = new List<TransactionDto>() { transactionDto };
            Entity entity = new() { LegalName = new() { Name = "legal_name" }, LegalAddress = new() { Country = "NL" } };
            Attributes attributes = new() { Lei = "LeiXX", Entity = entity };
            Root root = new Root() { Data = new List<Datum> { new Datum() { Attributes = attributes } } };

            _gleifApiRepositoryMock.Setup(api => api.GetDataByLeiAsync("LeiXX")).ReturnsAsync(root);

            List<Transaction> transactions = await _transactionService.GetTransactionsWithCostsAsync(transactionDtos);
            Assert.NotNull(transactions);
            Assert.NotEmpty(transactions);
            Assert.Equal(1, transactions.Count);

            Transaction transaction = transactions.First();

            Assert.Equal(transactionDto.Lei, transaction.Lei);
            Assert.Equal(transactionDto.Rate.Value, transaction.Rate);
            Assert.Equal(transactionDto.National.Value, transaction.National);
            Assert.Equal(entity.LegalName.Name, transaction.LegalName);
            Assert.Null(transaction.Bic);
            Assert.Equal(Math.Abs(decimal.Divide(transactionDto.National.Value, transactionDto.Rate.Value) - transactionDto.National.Value), transaction.Cost);
        }

        [Fact]
        public async Task ThrowExceptionForInvalidCountryTestAsync()
        {
            TransactionDto transactionDto = new TransactionDto() { Lei = "LeiXX", Rate = 0.1m, National = 20 };
            List<TransactionDto> transactionDtos = new List<TransactionDto>() { transactionDto };
            Entity entity = new() { LegalName = new() { Name = "legal_name" }, LegalAddress = new() { Country = "FR" } };
            Attributes attributes = new() { Lei = "LeiXX", Entity = entity };
            Root root = new Root() { Data = new List<Datum> { new Datum() { Attributes = attributes } } };

            _gleifApiRepositoryMock.Setup(api => api.GetDataByLeiAsync("LeiXX")).ReturnsAsync(root);

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _transactionService.GetTransactionsWithCostsAsync(transactionDtos));
            Assert.Equal("Invalid Country", ex.Message);
        }
    }
}