@echo off

:: Set paths
set VS_DEV_CMD="C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
set SUMMARY_PATH=C:\Users\Hashmat\source\repos\Magento\TestSummaryReport
set TEST_DLL=C:\Users\Hashmat\source\repos\Magento\Magento\bin\Debug\Magento.dll

:: Set the test category
set TEST_CATEGORY=FlowTest

:: Test result file name
set RESULT_FILE=%SUMMARY_PATH%\FlowTest_001.trx

:: Ensure the summary path exists
if not exist "%SUMMARY_PATH%" mkdir "%SUMMARY_PATH%"

:: Export the Visual Studio environment
echo Setting up Visual Studio environment...
call %VS_DEV_CMD%

:: Run the test using VSTest.Console.exe
echo Running tests...
vstest.console.exe "%TEST_DLL%" /TestCaseFilter:"TestCategory=%TEST_CATEGORY%" /Logger:"trx;LogFileName=%RESULT_FILE%"

cd C:\Users\Hashmat\source\repos\Magento\TestSummaryReport
TrxerConsole.exe %RESULT_FILE%

echo Test run completed.
pause
