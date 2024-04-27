using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Charts;
using Syncfusion.Blazor.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Cards;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.PivotView;
using AIS.Model.UserLogin;

namespace AdminDashboard.Server.Pages.OnlineExam
{
    public class OnlineTestDetailsBase: ComponentBase
    {
        [Parameter]
        public string Value { get; set; }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            int questionid = 0;
            var RowsData = sfOnlineTestQuestionlist.CurrentViewData;
            if (args.Item.Text == "Submit Answeer")
            {
                foreach (OnlineTestQuestionList row in RowsData)
                {
                    //Here you can get the state of the checkboxes in each row. Based on the ref value of each checkboxes in each rows 
                    // var b = InputCheck[row.OrderID].Checked;
                    questionid = row.QuestionId;
                }
            }
            if (args.Item.Text == "Back")
            {
                navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
            }
        }
        [Inject]
        NavigationManager navigationManager { get; set; }

        public SfGrid<OnlineTestQuestionList>  sfOnlineTestQuestionlist;
        
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public class OnlineTestQuestionList
        {
            public int QuestionId { get; set; }
            public string QuestionDetails { get; set; }
            public string Answeer_A { get; set; }
            public string Answeer_B { get; set; }
            public string Answeer_C { get; set; }
            public string Answeer_D { get; set; }
            public string ChooseAnsweer { get; set; }
        }
        public List<OnlineTestQuestionList> onlineTestQuestionLists = new List<OnlineTestQuestionList>();
        public OnlineTestQuestionList _onlineTestQuestionList { get; set; }
        public List<SelectAnsweer>  selectAnsweers = new List<SelectAnsweer>();
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            onlineTestQuestionLists = new List<OnlineTestQuestionList>()
        {
            new OnlineTestQuestionList()
            {
                 QuestionId=1001,
                  QuestionDetails="1Who among the following followed a policy of severe Control and punishment?",
                  Answeer_A="Camille Desmoulins",
                  Answeer_B="Jean Paul Marat ",
                  Answeer_C="Louis XVI",
                  Answeer_D="Maximilian Robespierre",
                  ChooseAnsweer="Not Selected"
                 
            },
             new OnlineTestQuestionList()
            {
                 QuestionId=1002,
                  QuestionDetails="Which one of the following countries was not included in Russian Empire?",
                  Answeer_A="Finland",
                  Answeer_B="JLithuania",
                  Answeer_C="Latvia",
                  Answeer_D="Austria",
                 ChooseAnsweer="Not Selected"

            },
              new OnlineTestQuestionList()
            {
                 QuestionId=1003,
                  QuestionDetails="Which of the following is the largest river of the peninsular plateau?",
                  Answeer_A="Godavari",
                  Answeer_B="Ganga",
                  Answeer_C="Krishna",
                  Answeer_D="Tapi",
                  ChooseAnsweer="Not Selected"

            },
               new OnlineTestQuestionList()
            {
                 QuestionId=1004,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"

            },
                new OnlineTestQuestionList()
            {
                 QuestionId=1005,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"

            },
                 new OnlineTestQuestionList()
            {
                 QuestionId=1006,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"

            },
                  new OnlineTestQuestionList()
            {
                 QuestionId=1007,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"

            },
                   new OnlineTestQuestionList()
            {
                 QuestionId=1008,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"
                   },
                  new OnlineTestQuestionList()
            {
                 QuestionId=1009,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"
                  },
                  new OnlineTestQuestionList()
            {
                 QuestionId=10010,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"
                  },
                  new OnlineTestQuestionList()
            {
                 QuestionId=10011,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"
            },
                  //=====
                  new OnlineTestQuestionList()
            {
                 QuestionId=10012,
                  QuestionDetails="1Who among the following followed a policy of severe Control and punishment?",
                  Answeer_A="Camille Desmoulins",
                  Answeer_B="Jean Paul Marat ",
                  Answeer_C="Louis XVI",
                  Answeer_D="Maximilian Robespierre",
                  ChooseAnsweer="Not Selected"

            },
             new OnlineTestQuestionList()
            {
                 QuestionId=10022,
                  QuestionDetails="Which one of the following countries was not included in Russian Empire?",
                  Answeer_A="Finland",
                  Answeer_B="JLithuania",
                  Answeer_C="Latvia",
                  Answeer_D="Austria",
                 ChooseAnsweer="Not Selected"

            },
              new OnlineTestQuestionList()
            {
                 QuestionId=10031,
                  QuestionDetails="Which of the following is the largest river of the peninsular plateau?",
                  Answeer_A="Godavari",
                  Answeer_B="Ganga",
                  Answeer_C="Krishna",
                  Answeer_D="Tapi",
                  ChooseAnsweer="Not Selected"

            },
               new OnlineTestQuestionList()
            {
                 QuestionId=10041,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"

            },
                new OnlineTestQuestionList()
            {
                 QuestionId=1005,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"

            },
                 new OnlineTestQuestionList()
            {
                 QuestionId=10061,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"

            },
                  new OnlineTestQuestionList()
            {
                 QuestionId=10071,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"

            },
                   new OnlineTestQuestionList()
            {
                 QuestionId=10081,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"
                   },
                  new OnlineTestQuestionList()
            {
                 QuestionId=10091,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"
                  },
                  new OnlineTestQuestionList()
            {
                 QuestionId=100101,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"
                  },
                  new OnlineTestQuestionList()
            {
                 QuestionId=100111,
                  QuestionDetails="Which one of the following is a feature of democracy?",
                  Answeer_A="Rule of one man",
                  Answeer_B="Power is exercised by army only",
                  Answeer_C="No freedom to express one’s opinion ",
                  Answeer_D="People can choose their rulers",
                 ChooseAnsweer="Not Selected"
            }
        };
            selectAnsweers = new List<SelectAnsweer>()
            {
                new SelectAnsweer(){ Text="Not Selected"},
                new SelectAnsweer(){ Text="A"},
                new SelectAnsweer(){ Text="B"},
                new SelectAnsweer(){ Text="C"},
                new SelectAnsweer(){ Text="D"},
                new SelectAnsweer(){ Text="Don't Know"},
            };
        }
        public class SelectAnsweer
        {
            //public string ID { get; set; }
            public string Text { get; set; }
           
        }
        public void OnChangeSelectAnsweer(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, SelectAnsweer> args)
        {
            try
            {
                if (args.ItemData.Text != "")
                {
                    var seletedAnsweer = args.ItemData.Text;
                    var value =  this.sfOnlineTestQuestionlist.GetSelectedRowCellIndexes();
                     
                    //_onlineTestQuestionList.ChooseAnsweer = seletedAnsweer;
                    //subj = _mapsubjectlistModel.Where(e => e.masterClassId == classid);
                }
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public void RowDataBound(RowDataBoundEventArgs<OnlineTestQuestionList> Args)

        {

            if (Args.Data.Answeer_A == "A")

            {

                Args.Row.AddClass(new string[] { "disablerow" });

            }

        }
    }
}
