using NUnit.Framework;

namespace ViewModel.Tests
{
    public class MenuViewModelTests
    {
        [Test]
        public void ExitCommandTest()
        {
            var vm = new MenuViewModel(null,null,null);
            Assert.AreEqual(true, vm.ExitCommand.CanExecute(null));
        }
        
        [Test]
        public void CloseCommandTest()
        {
            var vm = new MenuViewModel(null,null,null);
            Assert.AreEqual(true, vm.CloseCommand.CanExecute(null));
        }
        
        [Test]
        public void OpenCommandTest()
        {
            var vm = new MenuViewModel(null,null,null);
            Assert.AreEqual(true, vm.OpenCommand.CanExecute(null));
        }
    }
}