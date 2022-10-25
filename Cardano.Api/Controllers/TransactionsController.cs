using Cardano.Domain.Dto;
using Cardano.Domain.Entity;
using Cardano.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cardano.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ICsvFileService _csvFileService;
        private readonly ITransactionService _transactionService;
        private const string CsvExtension = ".csv";

        public TransactionsController(ICsvFileService csvFileService, ITransactionService transactionService)
        {
            _csvFileService = csvFileService;
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateCsvFileForTransactionsWithCostsAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Send a valid file please");

                if (string.IsNullOrEmpty(file.FileName))
                    return BadRequest("The file must have a name");

                if (Path.GetExtension(file.FileName) != CsvExtension)
                    return BadRequest("The file must be a csv");

                //read from Csv input file
                List<TransactionDto> transactionDtos = (await _csvFileService.LoadDataFromCsvFileAsync<TransactionDto>(file.OpenReadStream())).ToList();

                //Manipulate the data according to business rules
                List<Transaction> transactions = await _transactionService.GetTransactionsWithCostsAsync(transactionDtos);

                //generate Csv output file
                Byte[] bytes = await _csvFileService.CreateCsvFileFromDataAsync(transactions);

                string outPutFileName = file.FileName.Replace(CsvExtension, $"-result{CsvExtension}");
                return File(bytes, "text/plain", outPutFileName);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
