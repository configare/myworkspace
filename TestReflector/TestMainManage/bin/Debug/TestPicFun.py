import clr
import sys
sys.path.append(r'D:\documents\visual studio 2015\testpie3d\TestReflector\TestMainManage\bin\Debug')
clr.AddReferenceToFile("TestMainManage.dll")
from TestMainManage import *
if __name__=="__main__":
    t = PluginManage.GetxmlString('0001',r'D:\documents\visual studio 2015\testpie3d\TestReflector\TestMainManage\bin\Debug\CXargs.xml')
