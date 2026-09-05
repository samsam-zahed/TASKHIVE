using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TaskHive.Classes
{

    public class Job : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _id;
        public int id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(id)); }
        }

        //--------------------------------------------------------------------

        private string _datest;
        public string idday
        {

            get => _datest;
            set { _datest = value; OnPropertyChanged(nameof(idday)); }

        }

        //---------------------------------------------------------------------

        private string _customername;
        public string customername
        {
            get => _customername;
            set { _customername = value; OnPropertyChanged(nameof(customername)); }
        }

        //---------------------------------------------------------------------


        private string _jobcaption;
        public string jobcaption
        {
            get => _jobcaption;
            set { _jobcaption = value; OnPropertyChanged(nameof(jobcaption)); }
        }

        //---------------------------------------------------------------------


        private string _jobcomment;
        public string jobcomment
        {
            get => _jobcomment;
            set { _jobcomment = value; OnPropertyChanged(nameof(jobcomment)); }
        }

        //---------------------------------------------------------------------


        private string _jobsit;
        public string jobsit
        {
            get => _jobsit;
            set { _jobsit = value; OnPropertyChanged(nameof(jobsit)); }
        }

        //---------------------------------------------------------------------


        private string _jobduty;
        public string jobduty
        {
            get => _jobduty;
            set { _jobduty = value; OnPropertyChanged(nameof(jobduty)); }
        }

        //---------------------------------------------------------------------


        private string _jobtime;
        public string jobtime
        {
            get => _jobtime;
            set { _jobtime = value; OnPropertyChanged(nameof(jobtime)); }
        }

        //---------------------------------------------------------------------


        private string _jobenable;
        public string jobenable
        {
            get => _jobenable;
            set { _jobenable = value; OnPropertyChanged(nameof(jobenable)); }
        }

        //---------------------------------------------------------------------


        private int _idcustomer;
        public int idcustomer
        {
            get => _idcustomer;
            set { _idcustomer = value; OnPropertyChanged(nameof(idcustomer)); }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }

}
