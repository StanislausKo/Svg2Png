# Svg2Png
Command-line tool to (batch) convert SVG files to PNG  
Tired with looking for a passably functioning tool to batch convert SVG to PNG with size indication, I created a simple one of my own which seems to work (at least for me). It is a .NET program written in C#.  

The app depends on the SVG library by codeplex available (among other ways) via NuGet: https://www.nuget.org/packages/svg .  

 Supported call formats:  
  Conversion of a single SVG file:  
  svg2png -f svgFileName pngFileName {-w width} {-h height} {-d target directory}  
		svgFileName				: the name of the SVG file to convert  
		pngFileName				: the name of the resulting PNG file  
		{-w width}				: the width of the resulting PNG file (optional, if not found, will be set to 256)  
		{-h height}	: the height of the resulting PNG file (optional, if not found, will be set to 256)  
		{-d target directory}	: the folder into which to write the resulting PNG file (optional, if not found, the source folder is assumed)  
 
  Conversion of a number of SVG files by file name pattern (the names of the resulting PNG files are identical to the names of the SVG files):  
  svg2png -p svgFileNamePattern {-w width} {-h height} {-d target directory}  
		svgFileNamePattern		: the SVG file pattern (supports wildcharts)  
		{-w width}				: the width of the resulting PNG file (optional, if not found, will be set to 256)  
		{-h height}				: the height of the resulting PNG file (optional, if not found, will be set to 256)  
		{-d target directory}	: the folder into which to write the resulting PNG file (optional, if not found, the source folder is assumed)  
		
  Conversion of all files in the directory (the same as the latter format with the '*.svg' pattern)  
  svg2png -a {-w width} {-h height} {-d target directory}  
  {-w width}					: the width of the resulting PNG file (optional, if not found, will be set to 256)  
	 {-h height}				: the height of the resulting PNG file (optional, if not found, will be set to 256)  
	 {-d target directory}		: the folder into which to write the resulting PNG file (optional, if not found, the source folder is assumed)
   
