using System.Diagnostics;

namespace SendPass;

class Proggy
{
	static string	StripSingleQuotes(string singQuoted)
	{
		string	ret	=singQuoted;

		if(singQuoted.StartsWith('\''))
		{
			ret	=singQuoted.Substring(1);

			if(ret.EndsWith('\''))
			{
				ret	=ret.Substring(0, ret.Length - 1);
			}
		}
		return	ret;
	}


	static int	CallXDO(string args)
	{
		ProcessStartInfo	psi	=new ProcessStartInfo();

		psi.Arguments		=args;
		psi.FileName		="xdotool";
		psi.WindowStyle		=ProcessWindowStyle.Hidden;
		psi.CreateNoWindow	=true;

		Process	?proc	=Process.Start(psi);

		if(proc == null)
		{
			return	0;
		}

		proc.WaitForExit();

		return	proc.ExitCode;
	}


	//TODO: call wmctrl and find the window handle automagically
	static void Main(string[] args)
	{
		if(args.Length < 2 || args.Length > 2)
		{
			Console.WriteLine("Usage:  SendPass WindowID (AKA 0x0something from wmctrl -l) Password in single quotes");
			return;
		}

		string	windowID	=args[0];
		string	passWord	=StripSingleQuotes(args[1]);

		CallXDO("windowactivate " + windowID);

		//Console.WriteLine(passWord);

		CallXDO("type --window " + windowID + " " + passWord);

		return;
	}
}