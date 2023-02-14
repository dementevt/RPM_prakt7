using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
//firstTask
string pathToFile = "/Users/timofey/Documents/thirdCourse/RPM/RPM_prakt7/txtTask/cockBook.txt";
Dictionary<string, List<List<string>>> cookBook = addFileToCookBook(pathToFile);

Dictionary<string, List<List<string>>> addFileToCookBook(string pathToFile)
{
    string[] linesOfText = File.ReadAllLines(pathToFile);
    Dictionary<string, List<List<string>>> cookBook = new Dictionary<string, List<List<string>>>();
    int offset = 0, totalOffset = 0;
    bool endOfText = false;
    while (!endOfText)
    {
        string dishName = linesOfText[0 + offset];
        List<List<string>> composition = new List<List<string>>();
        for (int i = 0; i < Convert.ToInt32(linesOfText[1 + offset]); i++)
        {
            List<string> ingridient = new List<string>();

            ingridient.Add("ingredientName");
            int iOFS = linesOfText[2 + offset + i].IndexOf('|');                //indexOfFirstSeparator 
            ingridient.Add(linesOfText[2 + offset + i].Substring(0, iOFS - 1));

            ingridient.Add("quantity");
            int iOSS = linesOfText[2 + offset + i].IndexOf('|', iOFS + 1);      //indexOfSecondSeparator
            ingridient.Add(linesOfText[2 + offset + i].Substring(iOFS + 2, iOSS - iOFS - 3));

            ingridient.Add("measure");
            ingridient.Add(linesOfText[2 + offset + i].Substring(iOSS + 2, linesOfText[2 + offset + i].Length - iOSS - 2));

            totalOffset++;
            composition.Add(ingridient);
        }
        totalOffset += 2;
        cookBook.Add(dishName, composition);
        if (totalOffset == linesOfText.Length)
            endOfText = true;

        totalOffset += 1;
        offset = totalOffset;
    }
    return cookBook;
}
//secondTask
Dictionary<string, List<string>> getShopListByDishes(Dictionary<string, List<List<string>>> cookBook, List<string> dishes, int personCount)
{
    Dictionary<string, List<string>> shopList = new Dictionary<string, List<string>>();
    foreach (string dish in dishes)
    {
        foreach (List<string> ingridients in cookBook[dish])
        {
            if (shopList.ContainsKey(ingridients.ElementAt(1)))
            {
                int amountOfIngridient = Convert.ToInt32(shopList[ingridients.ElementAt(1)].ElementAt(3));
                amountOfIngridient += Convert.ToInt32(ingridients.ElementAt(3)) * personCount;
                shopList[ingridients.ElementAt(1)].RemoveAt(3);
                shopList[ingridients.ElementAt(1)].Add(amountOfIngridient.ToString());
            }
            else
            {
                List<string> lst = ingridients.GetRange(4, 2);
                lst.Add("quantity");
                lst.Add((Convert.ToInt32(ingridients.ElementAt(3)) * personCount).ToString());
                shopList.Add(ingridients.ElementAt(1), lst);
            }
        }
    }
    return shopList;
}


static void printDictionary(Dictionary<string, List<string>> ShopList)
{
    foreach(string ingridient in ShopList.Keys)
    {
        Console.Write(ingridient);
        foreach(string prop in ShopList[ingridient])
        {
            Console.Write($" {prop}");
        }
        Console.WriteLine("");
    }
    
}
printDictionary(getShopListByDishes(cookBook, new List<string> { "Omelette", "Fajitos" }, 2));

//thirdTask
List<string> listOfPaths = new List<string>();
listOfPaths.Add("/Users/timofey/Documents/thirdCourse/RPM/RPM_prakt7/txtTask//1.txt"); 
listOfPaths.Add("/Users/timofey/Documents/thirdCourse/RPM/RPM_prakt7/txtTask//2.txt");
listOfPaths.Add("/Users/timofey/Documents/thirdCourse/RPM/RPM_prakt7/txtTask//3.txt");
mergeFiles(listOfPaths);
void mergeFiles(List<string> listOfPaths)
{
    SortedDictionary<int, KeyValuePair<string, string[]>> filesInfo = new SortedDictionary<int, KeyValuePair<string, string[]>>();
    string allText = string.Empty;
    foreach (string path in listOfPaths) {
        filesInfo.Add(File.ReadAllLines(path).Length, new KeyValuePair<string, string[]>(path, File.ReadAllLines(path)));
    }
    foreach (int amountOfLines in filesInfo.Keys) {
        allText += filesInfo[amountOfLines].Key;
        allText += '\n';
        allText += amountOfLines;
        allText += '\n';
        allText += String.Join('\n', filesInfo[amountOfLines].Value);
        allText += "\n\n";
    }
    if (File.Exists("/Users/timofey/Documents/thirdCourse/RPM/RPM_prakt7/txtTask/finishFile.txt"))
    {
        File.WriteAllText("/Users/timofey/Documents/thirdCourse/RPM/RPM_prakt7/txtTask/finishFile.txt", allText);
    }
    else
    {
        File.Create("/Users/timofey/Documents/thirdCourse/RPM/RPM_prakt7/txtTask/finishFile.txt");
        File.WriteAllText("/Users/timofey/Documents/thirdCourse/RPM/RPM_prakt7/txtTask/finishFile.txt", allText);
    }
}


