using DebtProjectMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Controllers
{
    public static class RequestHelper
    {
        public static List<DebtEntryViewModel> GridSort(this List<DebtEntryViewModel> list, string column, string order)
        {
            switch(column.ToLower())
            {
                case "id":
                    if(order == "desc")
                        return list.OrderByDescending(x => x.Id).ToList();
                    else
                        return list.OrderBy(x => x.Id).ToList();
                case "name":
                    if (order == "name")
                        return list.OrderByDescending(x => x.Name).ToList();
                    else
                        return list.OrderBy(x => x.Name).ToList();
                case "surname":
                    if (order == "desc")
                        return list.OrderByDescending(x => x.Surname).ToList();
                    else
                        return list.OrderBy(x => x.Surname).ToList();
                case "pesel":
                    if (order == "desc")
                        return list.OrderByDescending(x => x.Pesel).ToList();
                    else
                        return list.OrderBy(x => x.Pesel).ToList();
                case "returnedvalue":
                    if (order == "desc")
                        return list.OrderByDescending(x => x.ReturnedValue).ToList();
                    else
                        return list.OrderBy(x => x.ReturnedValue).ToList();
                case "updated":
                    if (order == "desc")
                        return list.OrderByDescending(x => x.Updated).ToList();
                    else
                        return list.OrderBy(x => x.Updated).ToList();
                case "created":
                    if (order == "desc")
                        return list.OrderByDescending(x => x.Created).ToList();
                    else
                        return list.OrderBy(x => x.Created).ToList();
                case "status":
                    if (order == "desc")
                        return list.OrderByDescending(x => x.Status).ToList();
                    else
                        return list.OrderBy(x => x.Status).ToList();
                case "value":
                    if (order == "desc")
                        return list.OrderByDescending(x => x.Value).ToList();
                    else
                        return list.OrderBy(x => x.Value).ToList();
            }

            return list;
        }

        public static List<DebtEntryViewModel> GridFilter(this List<DebtEntryViewModel> list, string query)
        {
            query = query.ToLower();
            var filteredList = new List<DebtEntryViewModel>();

            foreach(var el in list)
            {
                if(el.GetType().GetProperties().Any(property => property.GetValue(el, null) != null && property.GetValue(el, null).ToString().ToLower().Contains(query)))
                {
                    filteredList.Add(el);
                }
            }

            return filteredList;
        }
    }
}
