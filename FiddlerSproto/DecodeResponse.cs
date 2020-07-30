using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fiddler;
using Standard;


namespace FiddlerSproto
{
    public class DecodeResponse : Inspector2, IResponseInspector2, IBaseInspector2, IWSMInspector 
    {
        private bool m_bDirty;
        private bool m_bReadOnly;
        private byte[] m_entityBody;
        private HTTPResponseHeaders m_ResponseHeaders;
        private JSONResponseViewer jsonResponseViewer;

        public DecodeResponse()
        {

        }

        public bool bDirty
        {
            get
            {
                return this.m_bDirty;
            }
        }

        public byte[] body
        {
            get
            {
                return jsonResponseViewer.body;
            }

            set
            {
                this.m_entityBody = value;
                byte[] decodedBody = this.DoDecryption();
                if (decodedBody != null)
                {
                    jsonResponseViewer.body = decodedBody;
                }
                else
                {
                    jsonResponseViewer.body = value;
                }
            }
        }

        public byte[] DoDecryption()
        {
            String base64Body = System.Text.Encoding.Default.GetString(this.m_entityBody);
            //此种方式转不了base64编码格式的字符串
            // String bodytext= Convert.ToBase64String(this.m_entityBody);
            String decryptionBody = "";
            byte[] decodeBody = System.Text.Encoding.UTF8.GetBytes(decryptionBody);
            return decodeBody;
        }


        public bool bReadOnly
        {
            get
            {
                return this.m_bReadOnly;
            }

            set
            {
                this.m_bReadOnly = value;
            }
        }

        public HTTPResponseHeaders headers
        {
            get
            {
                return this.m_ResponseHeaders;
            }

            set
            {

                this.m_ResponseHeaders = value;
                jsonResponseViewer.headers = value;
            }
        }

        public override void AddToTab(TabPage o)
        {
            jsonResponseViewer = new JSONResponseViewer();
            jsonResponseViewer.AddToTab(o);
            o.Text = "DecryptionFormatJson";
        }

        public void Clear()
        {
            this.m_entityBody = null;
            jsonResponseViewer.Clear();
        }

        public override int GetOrder()
        {
            return jsonResponseViewer.GetOrder();
        }

        public override int ScoreForContentType(string sMIMEType)
        {
            return jsonResponseViewer.ScoreForContentType(sMIMEType);
        }

        public override void SetFontSize(float flSizeInPoints)
        {
            jsonResponseViewer.SetFontSize(flSizeInPoints);
        }

        public void AssignMessage(WebSocketMessage oWSM)
        {
            byte[] data = oWSM.PayloadAsBytes();
            string path = string.Format("d:/{0}_sproto.data",oWSM.ID);
            System.IO.File.WriteAllBytes(path,data );

        }

    }
}
