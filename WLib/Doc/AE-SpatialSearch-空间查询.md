## 空间查询

空间查询主要涉及三个内容：

* 空间关系筛选 - SpatialFilter
* 空间关系判断 - Relational
* 空间拓扑分析 - Topological

### 空间关系筛选

该内容就是对`ArcEngine`接口`ISpatialFilter`对应功能的封装，使其成为`IFeatureClass`或`IFeatureLayer`的扩展方法

```C#
//筛选要素类中，包含指定图斑的要素
List<IFeature> features =  featureClass.FilterFeatures(
    geometry, esriSpatialRelEnum.esriSpatialRelContains, "FID < 100");
```

### 空间关系判断

空间关系包括：包含Contains、相交Crosses、相等Equals、相接Touches、内部Within、不相交Disjoint、重叠Overlaps

```C#
bool isOverlaps = geometryA.Overlaps(geometryB);//图斑A与图斑B是否重叠
bool isContains = geometryA.Contains(geometryB);//图斑A是否包含图斑B
bool isEquals = geometryA.Equals(geometryB);//图斑A与图斑B是否相同
bool isTouches = geometryA.Touches(geometryB);//图斑A与图斑B是否相接
bool isWithin = geometryA.Within(geometryB);//图斑A是否在图斑B内部
bool isDisjoint = geometryA.Disjoint(geometryB);//图斑A与图斑B是否不相交
bool isCrosses = geometryA.Crosses(geometryB);//图斑A与图斑B是否相交
```

### 空间拓扑分析

该内容主要是对`ArcEngine`接口`ITopologicalOperator`对应功能的封装，使其成为`IGeometry`或`IFeature`等接口的扩展方法

```C#
//获取指定图形的缓冲区
IGeometry bufferGeometry = geometry.GetBuffer();

// 将多个面图形分别与指定面图形进行相交，获得相交面积最大的要素
IGeometry intersectGeometry = geometries.GetMaxAreaIntersectGeometry(geometry);

//将多个图形合并(Union)成一个图形
IGeometry unionGeometry = geometries.UnionGeometryEx();
```

