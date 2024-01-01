using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using yarn_brokerage.Models;

namespace yarn_brokerage.ViewModels
{
    public class LedgerTeamNameViewModel : BaseViewModel
    {
        public class MyObject
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string IsDefault { get; set; }
        }
        public InfiniteScrollCollection<LedgerTeamName> LedgerTeamNames { get; set; }
        public Command LoadLedgersCommand { get; set; }
        public int _ledger_type;
        private string _SearchString = "";
        private const int PageSize = 10;
        private int totalRows = 0;
        public LedgerTeamNameViewModel(int ledger_type)
        {
            _ledger_type = ledger_type;
            if (ledger_type == 1)
                Title = "Supplier  Confirmed By";
            else
                Title = "Customer Confirmed By ";

            if (ledger_type == 0)
            {
                Title = "Team Name";
            }
            else if (ledger_type == 3)
            {
                Title = "Team Group Name";
            }

            LedgerTeamNames = new InfiniteScrollCollection<LedgerTeamName>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = LedgerTeamNames.Count / PageSize;

                    //var items = await _dataService.GetItemsAsync(page, PageSize);

                    using (var cl = new HttpClient())
                    {
                        HttpContent formcontent = null;
                        //if (_ledger_type == 1)
                        //{
                        //    formcontent = new FormUrlEncodedContent(new[]
                        //    {
                        //            new KeyValuePair<string,string>("ledger_type","1"),
                        //            new KeyValuePair<string,string>("search_string",_SearchString),
                        //            new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                        //            new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                        //        });
                        //}
                        //else if (_ledger_type == 2)
                        //{
                        //    formcontent = new FormUrlEncodedContent(new[]
                        //    {
                        //            new KeyValuePair<string,string>("ledger_type","2"),
                        //            new KeyValuePair<string,string>("search_string",_SearchString),
                        //            new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                        //            new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                        //        });
                        //}
                        //else
                        //{
                        //    formcontent = new FormUrlEncodedContent(new[]
                        //    {
                        //            new KeyValuePair<string,string>("search_string",_SearchString),
                        //            new KeyValuePair<string,string>("CurrentPageNumber",page.ToString()),
                        //            new KeyValuePair<string,string>("PageSize",PageSize.ToString())
                        //    });
                        //}

                        formcontent = new FormUrlEncodedContent(new[]
                            {
                                    new KeyValuePair<string,string>("team_name",_SearchString),

                            });

                        var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/team_get", formcontent);

                        var response = await request.Content.ReadAsStringAsync();

                        LedgerTeamName res = Newtonsoft.Json.JsonConvert.DeserializeObject<LedgerTeamName>(response);

                        IsBusy = false;
                        return (IEnumerable<LedgerTeamName>)res;
                    }
                    // return the items that need to be added

                },
                OnCanLoadMore = () =>
                {
                    return LedgerTeamNames.Count < totalRows;
                }
            };
            LoadLedgersCommand = new Command(async (object CountId) => await ExecuteLoadLedgersCommand(CountId));
        }
        async Task ExecuteLoadLedgersCommand(object searchstring)
        {
            if (IsBusy)
                return;

            _SearchString = "";
            if (searchstring != null)
                _SearchString = (string)searchstring;

            IsBusy = true;
            try
            {
                LedgerTeamNames.Clear();
                try
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                    new KeyValuePair<string,string>("team_name",_SearchString),

                            });
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/team_get", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            IEnumerable<LedgerTeamName> res = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<LedgerTeamName>>(response);

                            //MyObject MyObject = new MyObject();
                            //foreach (var item in res)
                            //{
                            //    MyObject.Id = item.Id;
                            //    MyObject.Name = item.Name;
                            //    MyObject.IsDefault = item.IsDefault;

                            //}
                            LedgerTeamNames.AddRange(res);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task ExecuteLoadLedgersGroupCommand(string searchString)
        {
            if (IsBusy)
                return;

            _SearchString = searchString;


            IsBusy = true;
            try
            {
                LedgerTeamNames.Clear();
                try
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;
                            formcontent = new FormUrlEncodedContent(new[]
                            {
                                    new KeyValuePair<string,string>("team_name",_SearchString),

                            });
                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/team_group_get", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            IEnumerable<LedgerTeamName> res = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<LedgerTeamName>>(response);


                            LedgerTeamNames.AddRange(res);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
