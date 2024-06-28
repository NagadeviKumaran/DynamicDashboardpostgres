using DynamicDashboardAspPostgres.Data;
using DynamicDashboardAspPostgres.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynamicDashboardAspPostgres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Charts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chart>>> GetCharts()
        {
            return await _context.Charts
                                 .Include(c => c.Data)
                                 .ThenInclude(d => d.Datasets)
                                 .ToListAsync();
        }

        // GET: api/Charts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chart>> GetChart(int id)
        {
            var chart = await _context.Charts
                                      .Include(c => c.Data)
                                      .ThenInclude(d => d.Datasets)
                                      .FirstOrDefaultAsync(c => c.Id == id);

            if (chart == null)
            {
                return NotFound();
            }

            return chart;
        }

        // POST: api/Charts
        [HttpPost]
        public async Task<ActionResult<Chart>> PostChart(Chart chart)
        {
            _context.Charts.Add(chart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChart", new { id = chart.Id }, chart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChart(int id, Chart chart)
        {
            if (id != chart.Id)
            {
                return BadRequest();
            }

            // Get the existing chart including related entities
            var existingChart = await _context.Charts
                                              .Include(c => c.Data)
                                              .ThenInclude(d => d.Datasets)
                                              .FirstOrDefaultAsync(c => c.Id == id);

            if (existingChart == null)
            {
                return NotFound();
            }

            // Update the chart properties
            _context.Entry(existingChart).CurrentValues.SetValues(chart);

            // Update the ChartData properties
            if (existingChart.Data != null && chart.Data != null)
            {
                _context.Entry(existingChart.Data).CurrentValues.SetValues(chart.Data);

                // Update the Datasets properties
                foreach (var existingDataset in existingChart.Data.Datasets.ToList())
                {
                    var updatedDataset = chart.Data.Datasets.FirstOrDefault(d => d.Id == existingDataset.Id);

                    if (updatedDataset != null)
                    {
                        _context.Entry(existingDataset).CurrentValues.SetValues(updatedDataset);
                    }
                    else
                    {
                        _context.Dataset.Remove(existingDataset);
                    }
                }

                foreach (var newDataset in chart.Data.Datasets.Where(d => d.Id == 0))
                {
                    existingChart.Data.Datasets.Add(newDataset);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Charts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChart(int id)
        {
            var chart = await _context.Charts
                                      .Include(c => c.Data)
                                      .ThenInclude(d => d.Datasets)
                                      .FirstOrDefaultAsync(c => c.Id == id);

            if (chart == null)
            {
                return NotFound();
            }

            // Remove related datasets first
            if (chart.Data != null)
            {
                _context.Dataset.RemoveRange(chart.Data.Datasets);
                _context.ChartData.Remove(chart.Data);
            }

            _context.Charts.Remove(chart);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool ChartExists(int id)
        {
            return _context.Charts.Any(e => e.Id == id);
        }
    }
}
