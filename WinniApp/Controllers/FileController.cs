using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace WinniApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _directory;
        private readonly StringBuilder _logBuilder = new StringBuilder();

        public FileController()
        {
            _directory = Environment.GetEnvironmentVariable("HOME") ?? ".";
        }

        [HttpGet]
        public string Get(string fileName, string fileContent)
        {
            _logBuilder.AppendLine($"{nameof(fileName)}: {fileName}");
            _logBuilder.AppendLine($"{nameof(fileContent)}: {fileContent}");
            _logBuilder.AppendLine($"{nameof(_directory)}: {_directory}");           

            Create(fileName, fileContent);

            return _logBuilder.ToString();
        }

        private void Create(string fileName, string fileContent)
        {
            Process pythonProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName= "py",
                    Arguments = $"app.py {_directory} {fileName} {fileContent}",
                    RedirectStandardOutput = true
                }
            };
            pythonProcess.Start();
            pythonProcess.WaitForExit(2000);
            string pythonOutput = pythonProcess.StandardOutput.ReadToEnd();
            _logBuilder.AppendLine($"{nameof(pythonOutput)}: {pythonOutput}");

            FileInfo file = new FileInfo(Path.Combine(_directory, fileName));
            
            if (file.Exists)
            {
                _logBuilder.AppendLine($"File created: {file.FullName}");
            }
            else
            {
                _logBuilder.AppendLine($"File not created: {file.FullName}");
            }
        }       
    }
}
