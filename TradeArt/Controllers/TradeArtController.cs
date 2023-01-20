using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Logging;
using TradeArt.Contracts;
using TradeArt.Contracts.GraphQL;
using TradeArt.Entities;
using TradeArt.Entities.DataProcessor;
using TradeArt.Entities.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TradeArt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradeArtController : ControllerBase
    {
        private readonly ITextInverter _textService;
        private readonly IHashCalculator _hashService;
        private readonly IAsyncWorker _asyncService;
        private readonly IGraphQLService _graphQLService;
        private readonly ILogger<TradeArtController> _logger;

        private static readonly string lorenIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                                                    sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                                                    Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris
                                                    nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in
                                                    reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                                                    Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
                                                    deserunt mollit anim id est laborum.";
        private readonly string _basePath;

        public TradeArtController(
            IAsyncWorker asyncService, IGraphQLService graphQLService,
            IHashCalculator hashService, ITextInverter textService
            )
        {
            _basePath = Path.GetFullPath(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, @"../../.."));
            _textService = textService;
            _graphQLService = graphQLService;
            _asyncService = asyncService;
            _hashService = hashService;
        }

        /// <summary>
        /// Invert lorem ipsum text.
        /// </summary>
        [HttpGet("invertText")]
        public IActionResult InvertText()
        {
            var result = _textService.InvertText(lorenIpsum);
            return new JsonResult(result);
        }

        /// <summary>
        /// invert lorem ipsum text or any other text.
        /// </summary>
        [HttpPost("invertText")]
        public IActionResult InvertText([FromBody] InvertRequest model)
        {
            if (string.IsNullOrEmpty(model.Text))
                return BadRequest(new { Error = "Text cannot be empty or null" });

            var response = new InvertResponse();
            var result = _textService.InvertText(model.Text);
            response.InvertedText = result;

            return Ok(response);
        }

        /// <summary>
        /// Given a function A that runs a loop of 1...1000 and emits some data (can be a
        /// number from 1 to 1000) and it needs to pass this data to a second function B that has a
        /// processing delay of this data of 0.1 second and returns true, when it has finished
        /// processing the data.Write code that will allow A to work as fast as it can without
        /// blocking, but still make sure that B has processed all of the data (of course, after B has
        /// finished).
        /// </summary>
        [HttpGet("worker")]
        public async Task<IActionResult> AsyncWorker()
        {
            var response = new AsyncResponse<bool>() { };
            var stopwatch = new Stopwatch();
            stopwatch.Start();


            response.Data = await _asyncService.FuncA();
            response.Success = true;


            stopwatch.Stop();
            response.Message = $"Data processed in {stopwatch.ElapsedMilliseconds} ms";

            return Ok(response);
        }
        /// <summary>
        /// Using any of these files (or you can choose your own): Test Files (hetzner.de)
        /// Calculate a SHA hash(in hex form) without fully keeping the files in memory
        /// at a single moment of time.
        /// </summary>
        [HttpGet("hash")]
        public async Task<IActionResult> FileHash()
        {
            var _dir = $"{_basePath}/Resources/100MB.bin";
            var hashed = await _hashService.CalculateShaAsync(_dir);
            return Ok(new { HashedFile = hashed });
        }

        /// <summary>
        /// Please write a function that fetches all the assets and
        /// then fetches the prices for the first 100 of them in batches of 20
        /// (e.g.total is 100, do 20 at the same time, another 20 and so on).
        /// </summary>
        [HttpGet("graphQuery")]
        public async Task<IActionResult> GraphQuery()
        {
            var assets = await _graphQLService.FetchAssetsWithPrices();
            return new JsonResult(assets);
        }
    }
}

