using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DebtProjectMVC.DebtDbContext;
using DebtProjectMVC.Entities;
using DebtProjectMVC.Models;
using DebtProjectMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DebtProjectMVC.Controllers
{
    [Route("api")]
    [ApiController]
    public class DebtApiController : ControllerBase
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly DebtDatabaseContext dbContext;

        public DebtApiController(
            UserManager<IdentityUser> userManager,
            DebtDatabaseContext dbContext
            )
        {
            this.UserManager = userManager;
            this.dbContext = dbContext;
        }

        [Route("for-borrower")]
        [HttpPost]
        public async Task<DebtEntriesViewModel> GetForBorrowers([FromBody] DebtBorrowerRequestModel request)
        {
            var rows = dbContext.Debts.Where(d => d.PESEL == request.PESEL).Select(d => new DebtEntryViewModel()
            {
                Id = d.Id,
                Created = d.Created,
                Updated = d.Updated,
                ReturnedValue = d.ReturnedValue,
                Value = d.Value,
                Status = Translator.Translate(d.Status),
                Name = d.Creditor.Name,
                Surname = d.Creditor.Surname,
            }).ToList();

            var rowCount = request.RowCount == -1 ? int.MaxValue : request.RowCount;           

            if(!string.IsNullOrEmpty(request.Search))
            {
                rows = rows.GridFilter(request.Search);
            }
            rows = rows.GridSort(request.SortBy, request.SortDirection);
            rows = rows.Take(rowCount).ToList();

            var toReturn = new DebtEntriesViewModel()
            {
                Current = (int)(request.Current),
                RowCount = rowCount,
                Total = rows.Count,
                Rows = rows.ToArray()
            };

            return toReturn;
        }

        [Route("for-creditor")]
        [HttpPost]
        [Authorize]
        public async Task<DebtEntriesViewModel> GetForCreditors([FromBody] DebtBorrowerRequestModel request)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return new DebtEntriesViewModel();

            var creditor = this.dbContext.Creditors.FirstOrDefault(c => c.UserId == user.Id);
            if(creditor == null)
            {
                throw new Exception("User has not been found");
            }

            var rows = dbContext.Debts.Where(d => d.CreditorId == creditor.Id).Select(d => new DebtEntryViewModel()
            {
                Id = d.Id,
                Created = d.Created,
                Updated = d.Updated,
                ReturnedValue = d.ReturnedValue,
                Value = d.Value,
                Status = Translator.Translate(d.Status),
                Name = d.Name,
                Surname = d.Surname,
                Pesel = d.PESEL
            }).ToList();

            var rowCount = request.RowCount == -1 ? int.MaxValue : request.RowCount;

            if (!string.IsNullOrEmpty(request.Search))
            {
                rows = rows.GridFilter(request.Search);
            }
            rows = rows.GridSort(request.SortBy, request.SortDirection);
            rows = rows.Take(rowCount).ToList();

            var toReturn = new DebtEntriesViewModel()
            {
                Current = (int)(request.Current),
                RowCount = rowCount,
                Total = rows.Count,
                Rows = rows.ToArray()
            };

            return toReturn;
        }

        [Route("delete-debt")]
        [HttpPost]
        [Authorize]
        public async Task DeleteDebt([FromBody] RemoveDebtRequest request)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);

            if (user == null)
                throw new Exception("User has not been found");

            var creditor = this.dbContext.Creditors.FirstOrDefault(c => c.UserId == user.Id);
            if (creditor == null)
            {
                throw new Exception("User has not been found");
            }

            var debt = dbContext.Debts.Where(d => d.Id == request.Id && d.CreditorId == creditor.Id).FirstOrDefault();

            if(debt == null)
                throw new Exception("Debt Entry has not been found");

            dbContext.Remove(debt);
            await dbContext.SaveChangesAsync();
        }

        [Route("post-new-debt")]
        [HttpPost]
        [Authorize]
        public async Task PostNewDebt([FromBody] NewDebtEntryViewModel request)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);

            if (user == null)
                throw new Exception("User has not been found");

            var creditor = this.dbContext.Creditors.FirstOrDefault(c => c.UserId == user.Id);
            if (creditor == null)
            {
                throw new Exception("User has not been found");
            }

            var entry = new DebtEntry {
                Created = DateTime.Now,
                CreditorId = creditor.Id,
                Name = request.Name,
                Surname = request.Surname,
                PESEL = request.Pesel,
                Status = DebtEntryStatus.Open,
                ReturnedValue = 0,
                Updated = null,
                Value = request.Value
            };

            await this.dbContext.Debts.AddAsync(entry);
            await dbContext.SaveChangesAsync();
        }

        [Route("update-debt")]
        [HttpPost]
        [Authorize]
        public async Task UpdateDebt([FromBody] UpdateDebtEntryViewModel request)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);

            if (user == null)
                throw new Exception("User has not been found");

            var creditor = this.dbContext.Creditors.FirstOrDefault(c => c.UserId == user.Id);
            if (creditor == null)
            {
                throw new Exception("User has not been found");
            }

            var status = DebtEntryStatus.Open;
            if (request.Value == request.ReturnedValue)
                status = DebtEntryStatus.Closed;
            else if (request.ReturnedValue != 0 && request.Value > request.ReturnedValue)
                status = DebtEntryStatus.PartiallyReturned;
            else if(request.Value <= request.ReturnedValue)
                status = DebtEntryStatus.Closed;

            var debt = dbContext.Debts.Where(d => d.Id == request.Id).FirstOrDefault();

            if(debt == null)
                throw new Exception("Debt Entry has not been found");

            debt.CreditorId = creditor.Id;
            debt.Name = request.Name;
            debt.Surname = request.Surname;
            debt.PESEL = request.Pesel;
            debt.Status = status;
            debt.ReturnedValue = request.ReturnedValue;
            debt.Updated = DateTime.Now;
            debt.Value = request.Value;

            await dbContext.SaveChangesAsync();
        }
    }
}
