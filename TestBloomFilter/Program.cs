﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace TestBloomFilter
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		internal static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
			Application.ThreadException += Application_ThreadException;
			AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}

		private static string ExceptionLogFilename = "Exception.log.txt";
		private static string ExceptionLogTimestamp = "yyyy'-'MM'-'dd @ HH':'mm':'ss tt";		

		private static void LogExceptionInformation(Exception ex)
		{
			string exceptionMessage = string.Format("Encountered a '{0}' Exception: \"{1}\"", ex.GetType().Name, ex.Message);
			string logMessage = string.Format("[{0}]\t-\t{1}{2}{2}", DateTime.Now.ToString(ExceptionLogTimestamp), exceptionMessage, Environment.NewLine);
			File.AppendAllText(ExceptionLogFilename, logMessage);
			MessageBox.Show(exceptionMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex == null) { return; }
			LogExceptionInformation(ex);
		}

		private static void CurrentDomain_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
		{
			LogExceptionInformation(e.Exception);
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogExceptionInformation(e.Exception);
		}
	}
}