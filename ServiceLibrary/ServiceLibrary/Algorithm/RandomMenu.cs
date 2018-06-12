﻿using ServiceLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Algorithm
{
    public class RandomMenu
    {
        Random r = new Random();
        Database db = new Database();
        string category;
        string menu = null;
        DateTime now = DateTime.Now;
        int date;

        public FoodData Random()
        {
            DataTable menuTable = db.GetFoodData();                 // 음식을 가져옵니다.
            DataTable latelyTable = db.GetRandomRequestedData(1);   // 어제 음식을 가져옵니다.
            DataTable percentageTable = db.GetPercentageData(1);    // 카테고리의 퍼센트를 가져옵니다.
            int Ran = r.Next(0,menuTable.Rows.Count);               // 행을 기준으로 랜덤을 돌리고 변수에 저장합니다.
            FoodData resultFood = new FoodData();

            // 5개 이상의 데이터가 있는지 확인하는 반복문입니다.
            date = latelyTable.Rows.Count;

            // 음식 랜덤을 돌리는 알고리즘입니다.
            while (true)
            {
                 // 5개 이상의 데이터가 있을 때, 조건과 비교하는 알고리즘 입니다.
                if (date > 5)
                {
                    // 최근 메뉴와 랜덤 메뉴 값을 비교합니다.
                    if (!menuTable.Rows[Ran]["음식 명"].ToString().Equals(latelyTable.Rows[0]["음식 명"].ToString()))
                    {
                        // 퍼센트 값을 비교합니다.
                        for (int i = 0; i < percentageTable.Rows.Count; i++)
                        {
                            string compareCategory = percentageTable.Rows[i]["CATEGORY"].ToString();
                            string comparePercentage = percentageTable.Rows[i]["PERCENTAGE"].ToString();
                            string resultCategory = menuTable.Rows[Ran]["카테고리 명"].ToString();
                            
                            if (compareCategory.Equals(resultCategory) && double.Parse(comparePercentage.Substring(0, comparePercentage.IndexOf('%'))) <= 30 )
                            {
                                menu = menuTable.Rows[Ran]["음식 명"].ToString();
                                category = menuTable.Rows[Ran]["카테고리 명"].ToString();

                                db.SetRandomRequestedData(now, category, menu);

                                resultFood.CategoryName = category;
                                resultFood.FoodName = menu;
                                return resultFood;
                            }
                        }

                    }
                }

                // 5개 이상의 데이터가 없을 때, 조건과 비교하는 알고리즘입니다.
                else
                {
                    // 최근 메뉴와 랜덤 메뉴 값을 비교합니다.
                    if (!menuTable.Rows[Ran]["음식 명"].ToString().Equals(latelyTable.Rows[0]["음식 명"].ToString()))
                    {

                        menu = menuTable.Rows[Ran]["음식 명"].ToString();
                        category = menuTable.Rows[Ran]["카테고리 명"].ToString();

                        db.SetRandomRequestedData(now, category, menu);

                        resultFood.CategoryName = category;
                        resultFood.FoodName = menu;
                        return resultFood;
                    }
                    
                }
                Ran = r.Next(0, menuTable.Rows.Count);

            }

        }

    }
}
