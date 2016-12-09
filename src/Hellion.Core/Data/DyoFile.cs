using Hellion.Core.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hellion.Core.Data
{
    public sealed class DyoFile
    {
        private byte[] fileData;
        
        public ICollection<DyoElement> Elements { get; private set; }

        public DyoFile(byte[] dyoData)
        {
            this.fileData = dyoData;
            this.Elements = new List<DyoElement>();
        }


        public void Read()
        {
            using (var memoryStream = new MemoryStream(this.fileData))
            using (var memoryReader = new BinaryReader(memoryStream))
            {
                while (true)
                {
                    DyoElement rgnElement = null;
                    int type = memoryReader.ReadInt32();

                    if (type == -1)
                        break;

                    if (type == 5)
                    {
                        rgnElement = new NpcDyoElement();
                        rgnElement.Read(memoryReader);
                    }

                    if (rgnElement != null)
                        this.Elements.Add(rgnElement);
                }
            }
        }
    }
    
    public class DyoElement
    {
        public float Angle { get; private set; }

        public Vector3 Axis { get; private set; }

        public Vector3 Position { get; private set; }

        public Vector3 Scale { get; private set; }

        public int Type { get; private set; }

        public int Index { get; private set; }

        public int Motion { get; private set; }

        public int IAInterface { get; private set; }

        public int IA2 { get; private set; }

        public DyoElement()
        {
            this.Axis = new Vector3();
            this.Position = new Vector3();
            this.Scale = new Vector3();
        }

        public virtual void Read(BinaryReader reader)
        {
            this.Angle = reader.ReadSingle();
            this.Axis.X = reader.ReadSingle();
            this.Axis.Y = reader.ReadSingle();
            this.Axis.Z = reader.ReadSingle();
            this.Position.X = reader.ReadSingle();
            this.Position.Y = reader.ReadSingle();
            this.Position.Z = reader.ReadSingle();
            this.Scale.X = reader.ReadSingle();
            this.Scale.Y = reader.ReadSingle();
            this.Scale.Z = reader.ReadSingle();
            this.Type = reader.ReadInt32();
            this.Index = reader.ReadInt32();
            this.Motion = reader.ReadInt32();
            this.IAInterface = reader.ReadInt32();
            this.IA2 = reader.ReadInt32();
        }
    }

    public class NpcDyoElement : DyoElement
    {
        public string Name { get; private set; }

        public NpcDyoElement()
            : base()
        {
        }

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);

            reader.ReadBytes(64); // Korean name
            reader.ReadBytes(32); // dialog name
            this.Name = Encoding.GetEncoding(0).GetString(reader.ReadBytes(32));
            this.Name = this.Name.Substring(0, this.Name.IndexOf('\0'));
            reader.ReadInt32(); // ??
            reader.ReadInt32(); // ??
        }
    }
}
