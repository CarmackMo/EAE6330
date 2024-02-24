using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



enum MyEnum
{
    TYPE1, TYPE2, TYPE3
}


[Serializable]
struct Config
{
    public float num1;
    public int num2;

    public MyEnum type1;


};


[Serializable]
struct Row
{
    public List<Config> configs;
};



public class Test : MonoBehaviour
{

    [SerializeField]
    private int width = 10;

    [SerializeField] 
    private int height = 0;


    [SerializeField]
    private List<Row> configs = new List<Row>(10);


}
