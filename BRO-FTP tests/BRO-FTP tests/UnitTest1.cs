using NUnit.Framework;
using BRO_FTP;
namespace BRO_FTP_tests
{
    public class Tests
    {

        [Test]
        public void ConnectionTestSuccess()
        {

            Listener l = new Listener();

            TcpClient client1;
            TcpClient client2;
            try
            {
                l.TCPListener();
                client1 = l.Connect("127.0.0.1", "4000");
                client2 = l.Connect("127.0.0.1", "4000");
                Assert.Pass("Test Success: Connection Established");
            }
            catch
            {
                Assert.Fail("Connection Test Success Failed: Exception thrown");
            }


        }


        [Test]
        public void ConnectionTestFail()
        {
            Listener l = new Listener();
            try
            {
                l.TCPListener();
                l.Connect("127.0.0.1", "4000");
                l.Connect("192.68.1.1", "2000");
                Assert.Fail("Test Failure: Exception should be thrown.");

            }
            catch
            {
                Assert.Pass("Connection Test Failure Success: Exception thrown.");
            }
        }

        [Test]
        public void SenderTest()
        {

        }

        [Test]
        public void ReceiverTest()
        {

        }

        [Test]
        public void FileIntegrityCheck()
        {
            //Checking to make sure the file sent is the same as the file received
        }

        [Test]
        public void FileListingTestSuccess()
        {

        }


        [Test]
        public void FileListingTestFail()
        {

        }

    }
}