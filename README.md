# GoPro7FileJoiner
Use this tool to automatedly join your segmented 4gb GoPro files losslessly using FFMPEG

This application is a "helper".  It determines which mp4 files are in the "series", and creates the necessary batch and input files to use ffmpeg to join the files together.
The output of this application is a batch script that will feed ffmpeg the pertinent information to join all of your files.

It has been tested with the file names of gopro7 and gopro8.

Requires Visual Studio 2019+ and .NET Core 3.1+

#1a, download FFMPEG https://www.gyan.dev/ffmpeg/builds/
#1b, you need to have FFMPEG installed (put it in a known directory) and add the FFMPEG "bin" to your windows "PATH" variable

#2, Compile this application.  drop this application "GoPro7FileJoiner.exe" into the directory where your gopro .mp4 files are.

#3, Double-click "GoPro7FileJoiner.exe" to run the application.  should take a fraction of a second to run

#4a, a folder is created called "gopro7FileJoiner-<GUID>".  
#4b, Open this folder and double-click the "run_me_to_join_files.bat"

#5, the joined files will be on the "gopro7FileJoiner-<GUID>\output" directory.
#5a, original files are unchanged.
