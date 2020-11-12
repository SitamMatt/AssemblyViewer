using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private TypeInfo STR = new("STRING");
        private TypeInfo INT = new TypeInfo("INT");

        private List<TypeInfo> _types = new List<TypeInfo>()
        {
            new ("STRING") {
            SubTypes = new List<TypeInfo>(){
                dummyTypeInfo
            }
            },
            new ("INT") {
            SubTypes = new List<TypeInfo>(){
                dummyTypeInfo
            }
            }
        };

        public RelayCommand<TypeInfo> ExpandCommand
        {
            get;
            private set;
        }

        private static readonly TypeInfo dummyTypeInfo = new("DUMMY");
        public MainViewModel()
        {
            STR.SubTypes.Add(INT);
            INT.SubTypes.Add(STR);
            ExpandCommand = new RelayCommand<TypeInfo>(param=>ExecuteExpandCommand(param), param => true);
        }

        private void ExecuteExpandCommand(TypeInfo typeName)
        {

            var index = typeName.Name != "STRING" ? STR : INT;
            typeName.SubTypes.Clear();
            typeName.SubTypes.Add(index with { SubTypes = new List<TypeInfo> { dummyTypeInfo } });
        }

        public List<TypeInfo> Types
        {
            get => _types;
            set
            {
                _types = value;
                RaisePropertyChanged(nameof(Types));
            }
        }
    }
}
