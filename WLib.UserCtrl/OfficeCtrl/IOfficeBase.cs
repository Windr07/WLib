/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

namespace WLib.UserCtrls.OfficeCtrl
{
    public interface IOfficeBase
    {
        bool HasFileLoaded { get; }
        string FileName { get; }
        void LoadFile(string fileName, int handleId, bool readOnly = true);
        void OnResize(int handleId);
        void Close();
        void Save();
    }
}
