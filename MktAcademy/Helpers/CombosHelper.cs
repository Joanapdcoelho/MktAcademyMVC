using MktAcademy.Data;
using MktAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MktAcademy.Helpers
{
    public class CombosHelper
    {
        private static MktAcademyContext db = new MktAcademyContext();


        public static List<DocumentType> GetDocumentTypes()
        {
            var DocumentTypes = db.DocumentTypes.ToList();
            DocumentTypes.Add(new DocumentType
            {
                DocumentTypeID = 0,
                Description = "[Select a type of document]"
            }) ;

            return DocumentTypes.OrderBy(d => d.Description).ToList();
        }

        public void Dispose() 
        { 
            db.Dispose(); 
        }


    }
}