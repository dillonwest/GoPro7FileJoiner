# GoPro7FileJoiner
Use this tool to automatedly join your segmented 4gb GoPro files losslessly using FFMPEG

This application is a "helper".  It determines which mp4 files are in the "series", and creates the necessary batch and input files to use ffmpeg to join the files together.

The output of this application is a batch script that will feed ffmpeg the pertinent information to join all of your files.

It has been tested with the file names of gopro7 and gopro8.

I could not find this functionality in the gopro Quik editor, and it was a pain to manually join the gopro files just to watch them.

#1, you need to have FFMPEG installed and FFMPEG needs to be in your "PATH" variables

#2, drop this application into the directory where your gopro .mp4 files are.

#3, run the application.  should take a fraction of a second to run

#4, a folder is created called "gopro7FileJoiner-<GUID>".  Open this folder and double-click the "run_me_to_join_files.bat"

#5, the joined files will be on the "gopro7FileJoiner-<GUID>\output" directory.
#5a, original files are unchanged.
