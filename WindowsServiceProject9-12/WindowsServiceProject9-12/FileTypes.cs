using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsServiceProject9_12
{
    class StandardFile
    {
        //fields
        private string _filepath;
        private FileInfo _fileObjInfo;
        private int _size;
        private string _location;
        private bool _readOnly;
        private bool _hidden;
        private string _extension;
        private string[] _ext;
        public string _name;
        private string[] _namesplit;
        public string _fullLocation;

        //constructor
        public StandardFile()
        {
            _filepath = "";
            _name = "";
            _size = 0;
            _location = "";
            _readOnly = false;
            _hidden = false;
            _extension = "";
            _fullLocation = "";
        }

        //filepath property
        public StandardFile(string FilePath)
        {
            _filepath = FilePath;
            _fileObjInfo = new FileInfo(FilePath);
            _size = (int)_fileObjInfo.Length;
            _location = FilePath;
            _ext = FilePath.Split('.');
            _extension = _ext[1];
            _namesplit = FilePath.Split('\\');
            _name = _namesplit[(_namesplit.Length - 1)].Replace(("." + _extension), "");
            _fullLocation = _ext[0].Replace(_name, "");
        }
    }

    class Audio : StandardFile
    {
        //field
        private string _artist;
        private string _album;
        private int _year;
        private int _length;
        private int _bitrate;

        //constructor
        public Audio()
        {
            _artist = "";
            _album = "";
            _year = 0;
            _length = 0;
            _bitrate = 0;
        }
    }

    class Video : StandardFile
    {
        //field
        public int _length;
        public int _frameWidth;
        public int _frameHeight;
        public int _bitrate;
        public int _fps;

        //constructor
        public Video()
        {
            _length = 0;
            _frameWidth = 0;
            _frameHeight = 0;
            _bitrate = 0;
            _fps = 0;
        }
    }

    class Image : StandardFile
    {

        //field
        private int _width;
        private int _height;
        private int _horizontalResolution;
        private int _verticalResolution;

        //constructor
        public Image()
        {
            _width = 0;
            _height = 0;
            _horizontalResolution = 0;
            _verticalResolution = 0;
        }
    }
    
}
