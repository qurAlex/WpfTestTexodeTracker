using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using OxyPlot.Axes;

namespace WpfTestTexodeTracker
{

    public class ApplicationViewModel : INotifyPropertyChanged
    {
        
        private List<Users>[] users = new List<Users>[30];
        public ObservableCollection<UserMonth> userMonths = new ObservableCollection<UserMonth>();
        public bool stepsChart;

        public ApplicationViewModel() 
        {
            JSONRead();
            
        }

        public void JSONRead()
        {
            try
            {
                for (int i = 0; i < 30; i++)
                {
                    users[i] = new List<Users>();
                    string fileName = $"../../../TestData/day{i + 1}.json";
                    string jsonString = File.ReadAllText(fileName);
                    Console.WriteLine(jsonString);
                    JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
                    JsonElement jsonElement = jsonDocument.RootElement;

                    for (int j = 0; j < jsonElement.GetArrayLength(); j++)
                    {
                        bool userFound = false;
                        users[i].Add(jsonElement[j].Deserialize<Users>()!);
                        for (int n = 0; n < userMonths.Count; n++)
                        {
                            int count = users[i].Count - 1;
                            if (userMonths[n].User.Contains(users[i][count].User))
                            {
                                userMonths[n].Steps[i] = users[i][count].Steps;
                                userMonths[n].Status[i] = users[i][count].Status;
                                userMonths[n].Rank[i] = users[i][count].Rank;
                                userFound = true;
                            }
                        }
                        if (userFound == false) userMonths.Add(new UserMonth(users[i][j].User, i, users[i][j].Steps, users[i][j].Rank, users[i][j].Status));
                    }

                }
                for (int i = 0; i < userMonths.Count; i++)
                {
                    userMonths[i].MiddleSteps = userMonths[i].Steps.Sum() / userMonths[i].Steps.Length;
                    userMonths[i].UpperSteps = userMonths[i].Steps.Max();
                    userMonths[i].LowerSteps = userMonths[i].Steps.Min();
                }
            }
            catch 
            { 
                var messageEror = "произошла ошибка";
                OnPropertyChanged("MessageEror");
            }
        }


        public void JSONWrite()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(selectedUser, options);

                string fileName = $"../../../Data/{selectedUser.User}.json";
                if (!Directory.Exists("../../../Data/")) Directory.CreateDirectory("../../../Data/");
                FileInfo fileInfo = new FileInfo(fileName);
                StreamWriter writer = fileInfo.AppendText();
                writer.WriteLine(jsonString);
            }
            catch
            {
                var messageEror = "нужно чуть больше времени";
                OnPropertyChanged("MessageEror");
            }
        }

        public void Chart()
        {
            
            this.MyModel = new PlotModel { Title = "Шаги" };
            

            if (stepsChart)
            {

                var line1 = new OxyPlot.Series.LineSeries()
                {
                    Color = OxyPlot.OxyColors.Blue,
                    StrokeThickness = 1,
                };

                for (int i = 0; i < selectedUser.Steps.Length; i++)
                {
                    line1.Points.Add(new OxyPlot.DataPoint(i + 1, selectedUser.Steps[i]));
                }
                this.MyModel.Series.Add(line1);

            }
            else
            {
                this.MyModel.Axes.Add(new LinearAxis());
                double x = 0.0;
                for (int i = 0; i < selectedUser.Steps.Length; i++)
                {
                    var histogramSeries = new HistogramSeries()
                    {
                        StrokeThickness = 1,
                        FillColor = OxyColors.LightBlue,
                        StrokeColor = OxyColors.LightCyan
                    };
                    if (selectedUser.UpperSteps == selectedUser.Steps[i]) histogramSeries.FillColor = OxyColors.LightGreen;
                    if (selectedUser.LowerSteps == selectedUser.Steps[i]) histogramSeries.FillColor = OxyColors.Orange;
                    histogramSeries.Items.Add(new HistogramItem(x + 0.0, x + 1, selectedUser.Steps[i], 1));
                    MyModel.Series.Add(histogramSeries);

                    x += 1;
                }
            }
            OnPropertyChanged("MyModel");
        }
        public PlotModel MyModel { get; private set; }

        private UserMonth selectedUser;
        public UserMonth SelectedUser
        {
            get
            {
                return this.selectedUser;
            }

            set
            {
                this.selectedUser = value;
                OnPropertyChanged("SelectedUser");
                Chart();
                JSONWrite();
            }
        }

        public bool DifferenceSteps(int value)
        {
                if (selectedUser != null && Math.Abs(selectedUser.MiddleSteps - value) > selectedUser.MiddleSteps * 0.2)
                    return true; else return false;
        }

        public bool StepsChart
        {
            set { stepsChart = value; }
        }

        public ObservableCollection<UserMonth> UserMonths
        {
            get
            {
                return this.userMonths;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            
        }
    }
}
