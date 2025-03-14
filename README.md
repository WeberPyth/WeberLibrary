## WeberLibrary

* This repo uses machine-translation to support EN

Welcome to the WeberLirbary repository

欢迎来到WeberLirbary仓库


All Weber Library packages, including `WeberLibrary` `WeberLibrary.Windows` and `WeberLibraryFramework` are all located in this repository

全部的WeberLibrary包，包括`WeberLibrary`、`WeberLibrary.Windows`以及`WeberLibraryFramework`均位于本仓库


### 目录 Directories

* [概述Resume](#概述Resume)
* [功能Functions](#功能Functions)
  * [对象映射帮助类ObjectMapHelper](#对象映射帮助类ObjectMapHelper)
  * [可枚举扩展IEnumableEx](#可枚举扩展IEnumableEx)
  * [枚举类扩展EnumEx](#枚举类扩展EnumEx)
  * [AES帮助类AESHelper](#AES帮助类AESHelper)

### 概述Resume

There is only a separate package called `WeberLibraryFramework` under  the .NET Framework.

本项目在.NET Framework下，仅有单独的包`WeberLibraryFramework`


In the .NET 6.0 + environment, this project was split to 2 packages, named `WeberLibrary` and `WeberLibrary.Windows`

在.NET 6.0 + 环境下，本项目被拆分为两个包，`WeberLibrary` 和 `WeberLibrary.Windows`。

* `WeberLibrary`包含.NET环境下的函数  `WeberLibrary` contains Functions in the .NET environment

* `WeberLibrary.Windows`分流了仅在Windows平台下受支持的函数  `WeberLibrary.Windows` has diverted functions that are only supported Windows platform


### 功能Functions

#### 对象映射帮助类ObjectMapHelper

Faster than reflection: in In the `NET6.0+` environment, the MapHelper implemented by the expression tree is 10+times faster than the reflection implementation

比反射更快：在`.NET6.0+`环境下，表达式树实现的MapHelper比反射实现快了10+倍

定义Define
```csharp
public static TOut MapObj(TIn t)
```

使用Usage
```csharp

// class define
public class Tom 
{
    public string name;
    public int age;
}

public class Jack
{
    public string name;
    public int age;
}
```

```csharp
var tom = new Tom();
tom.name = "Tom";
tom.age = 20;
Jack jack = new Jack();
jack = ObjectMapHelper<Tom, Jack>.MapObj(tom);
```

#### 可枚举扩展IEnumableEx

##### 切块 Chunk By

IEnumable extension method `ChunkBy`, partitions according to the target size. The return value is an IEnumable object composed of blocked of the target size.

枚举扩展方法`ChunkBy`, 按指定大小进行分块。返回值为由指定大小块组成的IEnumable对象

定义Define
```csharp
public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
```

使用Usage
```csharp
// ChunkBy
IEnumerable<int> myList = new List<int> { 1,2,3,4,5,6,7,8,9,10 };
var chunkResult = myList.ChunkBy(2);
```

#### 枚举类扩展EnumEx

##### 存在交集 Has Intersection

Compare whether there is intersection between two Enum objects

判断两个枚举对象是否存在交集

定义Define
```csharp
public static bool HasIns<T>(this T o, T t)where T: Enum 
```

使用Usage
```csharp
//Enum define
[System.Flags]
public enum MyEnumType 
{
    Alpha = 1,
    Beta = 1 << 1,
    Gamma = 1 << 2,
}
```
```csharp
// Enum
MyEnumType origin = MyEnumType.Alpha | MyEnumType.Beta;
MyEnumType target = MyEnumType.Beta | MyEnumType.Gamma;
bool hasIns = origin.HasIns(target);
```


注意事项Attention

Only `.NET6.0+`：This extension method is implemented using an expression tree. Therefore, when executing this extension method globally for the first time, it will take an additional few tens of milliseconds to compile the expression tree. After compilation, each subsequent execution of this extension method will be much more efficient than the native method.

仅限`.NET 6.0+`：本扩展方法采用了表达式树实现。因此在全局首次执行本扩展方法时，会额外花费几十毫秒的时间进行一次表达式树编译。在完成编译后，之后的每次执行本扩展方法将会以远超原生方法的效率执行。


##### 是否为子集 Has Subset

Determine whether the current set has a subset that is the same as the target set

判断目标集合是否为当前集合的子集

定义Define
```csharp
public static bool HasSubset<T>(this T o, T t) where T : Enum
```

使用Usage
```csharp
MyEnumType origin = MyEnumType.Alpha | MyEnumType.Beta;
MyEnumType target = MyEnumType.Beta | MyEnumType.Gamma;

origin.HasSubset(target);
```


注意事项Attention

ditto
同上

##### 获取交集 Get Intersection

定义Define
```csharp
public static T GetIns<T>(this T o, T t) where T : Enum
```

使用Usage
```csharp
MyEnumType origin = MyEnumType.Alpha | MyEnumType.Beta;
MyEnumType target = MyEnumType.Beta | MyEnumType.Gamma;

var result = origin.GetIns(target);
```

#### AES帮助类AESHelper

##### 字符串编码为AES String Encrypt || 字符串解码AES String Decrypt


定义Define
```csharp
public static string Encrypt(string str, string key, string iv)
```
```csharp
public static string Decrypt(string str, string key, string iv)
```

使用Usage
```csharp
string originStr = "Hello, World!";
string key = "WeberLibrary";
string iv = "Weber";
var b64str = AESHelper.Encrypt(originStr, key, iv);
var rs = AESHelper.Decrpt(b64str, key, iv);
```


注意事项Attention

There are multiple overloads for encryption and decryption functions. You can check it yourself

加密和解密函数存在多种重载。您可自行查阅