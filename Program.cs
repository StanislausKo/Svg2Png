using System;
using System.Drawing;
using System.IO;
using Svg;

namespace Svg2Png
{
	class Program
	{
		private static string _helpText =
@"
SVG to PNG. 
Converts SVG file from the directory in which the utility is started.
Supported call formats:

    Conversion of a single SVG file:
    svg2png -f svgFileName pngFileName {-w width} {-h height} {-d target directory}
        svgFileName             : the name of the SVG file to convert
        pngFileName             : the name of the resulting PNG file
        {-w width}              : the width of the resulting PNG file (optional, default: 256)
        {-h height}             : the height of the resulting PNG file (optional, default: 256)
        {-d target directory}   : the folder into which to write the resulting PNG file (default: the source folder)

        Examples: svg2png -f test.svg test.png
                  svg2png -f test.svg test.png 32 32
                  svg2png -f test.svg test.png C:\temp


    Conversion of a number of SVG files by file name pattern (the names of the PNG files are the names of the SVG files):
    svg2png -p svgFileNamePattern {-w width} {-h height} {-d target directory}
        svgFileNamePattern      : the SVG file pattern (supports wildcharts)
        {-w width}              : the width of the resulting PNG file (optional, default: 256)
        {-h height}             : the height of the resulting PNG file (optional, default: 256)
        {-d target directory}   : the folder into which to write the resulting PNG file (default: the source folder)

        Examples: svg2png -p t*.svg test.png
                  svg2png -p t*.svg test.png 32 32
                  svg2png -p t*.svg test.png C:\temp

    Conversion of all files in the directory (the same as the latter format with the '*.svg' pattern)
        svg2png -a {-w width} {-h height} {-d target directory}
        {-w width}              : the width of the resulting PNG file (optional, default: 256)
        {-h height}             : the height of the resulting PNG file (optional, default: 256)
        {-d target directory}   : the folder into which to write the resulting PNG file (default: the source folder)

        Examples: svg2png -a 
                  svg2png -a 32 32
                  svg2png -a C:\temp

	Press any key to quit.
";

		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">
		/// Supported call formats:
		///  Conversion of a single SVG file:
		///  svg2png -f svgFileName pngFileName {-w width} {-h height} {-d target directory}
		///		svgFileName				: the name of the SVG file to convert
		///		pngFileName				: the name of the resulting PNG file
		///		{-w width}				: the width of the resulting PNG file (optional, if not found, will be set to 256)
		///		{-h height}	: the height of the resulting PNG file (optional, if not found, will be set to 256)
		///		{-d target directory}	: the folder into which to write the resulting PNG file (optional, if not found, the source folder is assumed)
		/// 
		///  Conversion of a number of SVG files by file name pattern (the names of the resulting PNG files are identical to the names of the SVG files):
		///  svg2png -p svgFileNamePattern {-w width} {-h height} {-d target directory}
		///		svgFileNamePattern		: the SVG file pattern (supports wildcharts)
		///		{-w width}				: the width of the resulting PNG file (optional, if not found, will be set to 256)
		///		{-h height}				: the height of the resulting PNG file (optional, if not found, will be set to 256)
		///		{-d target directory}	: the folder into which to write the resulting PNG file (optional, if not found, the source folder is assumed)
		///		
		///  Conversion of all files in the directory (the same as the latter format with the '*.svg' pattern)
		///  svg2png -a {-w width} {-h height} {-d target directory}
		///  {-w width}					: the width of the resulting PNG file (optional, if not found, will be set to 256)
		///	 {-h height}				: the height of the resulting PNG file (optional, if not found, will be set to 256)
		///	 {-d target directory}		: the folder into which to write the resulting PNG file (optional, if not found, the source folder is assumed)
		/// </param>
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Finish();
				return;
			}

			int width				= 256;
			int height				= 256;
			string targetDirectory	= Directory.GetCurrentDirectory();

			switch (args[0])
			{
				case "-f":
					if (args.Length < 3)
					{
						Finish();
					}

					string svgFileName		= args[1];
					string pngFileName		= args[2];

					if (args.Length >= 4)
					{
						try
						{
							width			= Int32.Parse(args[3]);
						}
						catch (Exception)
						{
							Console.WriteLine(@"Error in the 'width' entry: {args[3]}: quitting");
							return;
						}
					}

					if (args.Length >= 5)
					{
						try
						{
							height			= Int32.Parse(args[4]);
						}
						catch (Exception)
						{
							Console.WriteLine(@"Error in the 'height' entry: {args[4]}: quitting");
							return;
						}
					}

					if (args.Length >= 6)
					{
						targetDirectory	= args[5];
					}

					ConvertSingleFile(svgFileName, pngFileName, width, height, targetDirectory);

					break;

				case "-p":
					if (args.Length < 2)
					{
						Finish();
					}

					string svgFilePattern	= args[1];

					if (args.Length >= 3)
					{
						try
						{
							width			= Int32.Parse(args[2]);
						}
						catch (Exception)
						{
							Console.WriteLine(@"Error in the 'width' entry: {args[2]}: quitting");
							return;
						}
					}

					if (args.Length >= 4)
					{
						try
						{
							height			= Int32.Parse(args[3]);
						}
						catch (Exception)
						{
							Console.WriteLine(@"Error in the 'height' entry: {args[3]}: quitting");
							return;
						}
					}

					if (args.Length >= 5)
					{
						targetDirectory	= args[4];
					}

					ConvertFiles(svgFilePattern, width, height, targetDirectory);

					break;

				case "-a":
					if (args.Length >= 2)
					{
						try
						{
							width			= Int32.Parse(args[1]);
						}
						catch (Exception)
						{
							Console.WriteLine(@"Error in the 'width' entry: {args[1]}: quitting");
							return;
						}
					}

					if (args.Length >= 3)
					{
						try
						{
							height			= Int32.Parse(args[2]);
						}
						catch (Exception)
						{
							Console.WriteLine(@"Error in the 'height' entry: {args[2]}: quitting");
							return;
						}
					}

					if (args.Length >= 4)
					{
						targetDirectory	= args[3];
					}

					ConvertFiles("*.svg", width, height, targetDirectory);

					break;

				default:
					break;
			}
		}

		private static void ConvertFiles(string svgFilePattern, int width, int height, string targetDirectory)
		{
			foreach (string svgFileName in Directory.GetFiles(Directory.GetCurrentDirectory(), svgFilePattern))
			{
				string pngFileName	= $"{Path.GetFileNameWithoutExtension(svgFileName)}.png";
				ConvertSingleFile(svgFileName, pngFileName, width, height, targetDirectory);
			}
		}

		private static void ConvertSingleFile(string svgFileName, string pngFileName, int width, int height, string targetDirectory)
		{
			SvgDocument svgDocument = SvgDocument.Open(svgFileName);
			Bitmap bitmap			= svgDocument.Draw(width, height);

			try
			{
				string path			= Path.Combine(targetDirectory, pngFileName);
				bitmap.Save(path);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private static void Finish()
		{
			Console.WriteLine(_helpText);
			Console.ReadKey();
			return;
		}
	}
}
