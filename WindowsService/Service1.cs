using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using

using FlightFinder;

namespace WindowsService
{
	public partial class Service1 : ServiceBase
	{
		private Flight _flight = new Flight();

		public Service1()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			_flight.CheckFlights();
		}

		protected override void OnStop()
		{

		}
	}
}
