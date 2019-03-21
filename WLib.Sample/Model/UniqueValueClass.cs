using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ESRI.ArcGIS.Geodatabase;


namespace GISsys
{
    class UniqueValueClass
    {
        private int _UniqueValueCount;

        public Int32 Count { get { return this._UniqueValueCount; } }

        public UniqueValueClass()
        {
            _UniqueValueCount = 0;
        }

        public UniqueValueClass(IFeatureClass featureClass, string fieldName)
        {
            //IFeatureClass featureClass, string fieldName, out int count
            GetLayerUniqueFieldValueByDataStatistics( featureClass, fieldName);
        }

        /// <summary>
        /// 通过IQueryDef获取图层指定字段唯一值
        /// </summary>
        /// <param name="pFeatureLayer"></param>
        /// <param name="fieldName"></param>
        /// <returns>指定字段所有唯一值</returns>
        public ArrayList GetLayerUniqueFieldValueByQueryDef(IFeatureClass featureClass, string fieldName)
        {
            ArrayList arrValues = new ArrayList();
            IQueryDef pQueryDef = null;
            IRow pRow = null;
            ICursor pCursor = null;
            IFeatureWorkspace pFeatWrok = null;
            IDataset pDataset = null;
            pDataset = (IDataset)featureClass;
            pFeatWrok = (IFeatureWorkspace)pDataset.Workspace;
            pQueryDef = pFeatWrok.CreateQueryDef();
            pQueryDef.Tables = pDataset.Name;
            pQueryDef.SubFields = "DISTINCT(" + fieldName + ")";
            pCursor = pQueryDef.Evaluate(); pRow = pCursor.NextRow();
            while (pRow != null)
            {
                object pObj = pRow.get_Value(0);
                arrValues.Add(pObj.ToString());
                pRow = pCursor.NextRow();
            }
            arrValues.Sort();
            this._UniqueValueCount = arrValues.Count;
            return arrValues;
        }
        /// <summary>
        /// 通过IDataStatistics获取图层指定字段唯一值        
        /// </summary>        
        /// <param name="pFeatureLayer"></param>        
        /// <param name="fieldName"></param>        
        /// <returns>指定字段所有唯一值</returns>        
        public  ArrayList GetLayerUniqueFieldValueByDataStatistics(IFeatureClass featureClass, string fieldName)
        {
            ArrayList arrValues = new ArrayList();
            IQueryFilter pQueryFilter = new QueryFilterClass();
            IFeatureCursor pFeatureCursor = null;
            pQueryFilter.SubFields = fieldName;
            pFeatureCursor = featureClass.Search(pQueryFilter, true);
            IDataStatistics pDataStati = new DataStatisticsClass();
            pDataStati.Field = fieldName;
            pDataStati.Cursor = (ICursor)pFeatureCursor;
            IEnumerator pEnumerator = pDataStati.UniqueValues;
            pEnumerator.Reset(); while (pEnumerator.MoveNext())
            {
                object pObj = pEnumerator.Current;
                arrValues.Add(pObj.ToString());
            }
            arrValues.Sort();
            this._UniqueValueCount = arrValues.Count;
            return arrValues;
        }

    }
}
