
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

class Program
{
    public class subject
    {
        public string subjectName;
        public string dateStart;
        public string dateEnd;
        public int credit;
    }
    public class subjectCacul
    {
        public string subjectName;
        public int appear;
        public int dayStart;
    }
    public class room
    {
        public string roomName;
    }
    public class checkdays
    {
        public bool ca1, ca2, ca3, ca4;
    }
    // Caculating total days to study
    static int caculateDays(string startDateStr, string endDateStr)
    {
        DateTime startDate, endDate;

        if (DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate) &&
            DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
        {
            int daysDifference = (int)(endDate - startDate).TotalDays;
            return Math.Abs(daysDifference);
        }

        return -1;
    }

    // Counting time to appeat in week
    static int caculateFrequency(int credit, subject sb)
    {
        int totalDays = caculateDays(sb.dateStart, sb.dateEnd);
        Console.WriteLine("total: {0}", totalDays);
        int weeks = totalDays / 7;
        int lesson = credit * 5;
        int appear = lesson / weeks;
        Console.WriteLine("appear: {0}", appear);
        return appear;
    }
    // get day off week
    static int DayOfWeek(string dateStart)
    {
        if (TryParseDate(dateStart, out int day, out int month, out int year))
        {
            return GetDayOfWeek(year, month, day);
        }

        return -1;
    }
    static bool TryParseDate(string input, out int day, out int month, out int year)
    {
        day = month = year = 0;
        string[] dateParts = input.Split('/');

        // Hàm int.TryParse chuyển đổi phần tử theo index của mảng dateParts thành một số nguyên và gán giá trị này cho biến day,month,year
        if (dateParts.Length == 3 && int.TryParse(dateParts[0], out day) && int.TryParse(dateParts[1], out month) && int.TryParse(dateParts[2], out year))
        {
            return true;
        }

        return false;
    }
    static int GetDayOfWeek(int year, int month, int day)
    {
        // Kiểm tra nếu ngày không hợp lệ
        if (!IsValidDate(year, month, day))
        {
            throw new ArgumentException("Ngày không hợp lệ.");
        }

        // Tạo đối tượng DateTime từ ngày cần kiểm tra
        DateTime date = new DateTime(year, month, day);

        // Lấy thứ của ngày và ép kiểu sang int (từ 0 - Chủ nhật đến 6 - Thứ 7)
        int dayOfWeek = (int)date.DayOfWeek;

        // Chuyển đổi từ hệ thống thứ trong tuần (từ 0 - Chủ nhật đến 6 - Thứ 7) sang thứ trong tuần thông thường (từ 1 - Chủ nhật đến 7 - Thứ 7)
        if (dayOfWeek == 0)
        {
            dayOfWeek = 7;
        }
        //DayOfWeek.Sunday: Chủ nhật(tương ứng với giá trị 0)
        //DayOfWeek.Monday: Thứ hai(tương ứng với giá trị 1)
        //DayOfWeek.Tuesday: Thứ ba(tương ứng với giá trị 2)
        //DayOfWeek.Wednesday: Thứ tư(tương ứng với giá trị 3)
        //DayOfWeek.Thursday: Thứ năm(tương ứng với giá trị 4)
        //DayOfWeek.Friday: Thứ sáu(tương ứng với giá trị 5)
        //DayOfWeek.Saturday: Thứ bảy(tương ứng với giá trị 6)
        else
        {
            dayOfWeek += 1;
        }

        return dayOfWeek;
    }

    static bool IsValidDate(int year, int month, int day)
    {
        // Kiểm tra nếu năm, tháng và ngày nằm trong khoảng hợp lệ
        if (year < 1 || month < 1 || month > 12 || day < 1 || day > DateTime.DaysInMonth(year, month))
        {
            return false;
        }

        return true;
    }

    // Return subjectCacul Object
    static subjectCacul caculateSubject(subject sb)
    {
        subjectCacul result = new subjectCacul();
        result.subjectName = sb.subjectName;
        result.appear = caculateFrequency(sb.credit, sb);
        result.dayStart = DayOfWeek(sb.dateStart);
        return result;
    }
    //-------------------
    // First sub
    static int IsClassMoreThanClasses(int[,] result, int classes, int i, int j)
    {
        if (result[i, j] > classes)
        {
            result[i, j] = 0;
        }
        return result[i, j];
    }
    static int[,] softFirst1(int[,] a, int classes)
    {
        int rows = 4;
        int cols = 6;
        int clas = 1;
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (clas == 3)
                {
                    if (classes == 24)
                    {
                        result[i, j] = 24;
                        clas++;
                    }
                    else
                    {
                        result[i, j] = 0;
                        clas++;
                    }
                }
                else if (i == 2 && j == 1)
                {
                    if (classes >= 3)
                    {
                        result[i, j] = 3;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (clas <= classes)
                {
                    result[i, j] = clas;
                    clas++;
                }

                else
                {
                    result[i, j] = 0;
                }
            }
        }
        a = result;
        return a;
    }
    static int[,] softFirst2(int[,] a, int classes)
    {

        int rows = 4;
        int cols = 6;
        int clas = 1;
        int[,] result = new int[rows, cols];


        for (int i = 0; i < rows; i++)
        {
            for (int j = cols - 1; j >= 0; j--)
            {
                if ((i == 1 && j == 4) || (i == 2 && j == 3) || (i == 3 && j == 2))
                {
                    result[i, j] = clas;
                    IsClassMoreThanClasses(result, classes, i, j);
                    clas++;
                }
                else if (i == 2 && j == 1)
                {
                    result[i, j] = 4;
                    IsClassMoreThanClasses(result, classes, i, j);
                }

                else if (i == 1 && j == 0)
                {
                    result[i, j] = 5;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if ((i == 2 && j == 4) || (i == 3 && j == 3))
                {
                    result[i, j] = clas + 4;
                    IsClassMoreThanClasses(result, classes, i, j);

                }
                else if ((i == 2 && j == 2))
                {

                    result[i, j] = 8;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 1)
                {
                    result[i, j] = 9;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 0)
                {
                    result[i, j] = 10;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 5)
                {
                    result[i, j] = 11;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 4)
                {
                    result[i, j] = 12;
                    IsClassMoreThanClasses(result, classes, i, j);
                }

                else if (i == 1 && j == 3)
                {
                    result[i, j] = 13;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 4)
                {
                    result[i, j] = 14;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 5)
                {
                    result[i, j] = 15;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 3)
                {
                    result[i, j] = 16;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 2)
                {
                    result[i, j] = 17;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 2)
                {
                    result[i, j] = 18;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 0)
                {
                    result[i, j] = 19;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 1)
                {
                    result[i, j] = 20;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 5)
                {
                    result[i, j] = 21;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 5)
                {
                    result[i, j] = 22;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 1)
                {
                    result[i, j] = 23;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 0)
                {
                    result[i, j] = 24;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else
                {
                    result[i, j] = 0;
                }
            }

        }
        return result;
    }
    static int[,] softFirst3(int[,] a, int classes, bool flag)
    {
        int rows = 4;
        int cols = 6;
        int clas = 1;
        int[,] result = new int[rows, cols];


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i == 0 && j == 0)
                {
                    if (classes >= 16)
                    {
                        result[i, j] = 16;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }

                }
                else if (i == 0 && j == 1)
                {
                    if (classes >= 17)
                    {
                        result[i, j] = 17;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }

                else if (i == 0 && j == 2)
                {
                    result[i, j] = 1;
                }
                else if (i == 0 && j == 3)
                {
                    if (classes >= 21)
                    {
                        result[i, j] = 21;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 0 && j == 4)
                {
                    if (classes >= 15)
                    {
                        result[i, j] = 15;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 0 && j == 5)
                {
                    if (classes >= 11)
                    {
                        result[i, j] = 11;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 1 && j == 0)
                {
                    if (classes >= 4)
                    {
                        result[i, j] = 4;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 1 && j == 1)
                {
                    if (classes >= 18)
                    {
                        result[i, j] = 18;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 1 && j == 2)
                {
                    if (classes >= 19)
                    {
                        result[i, j] = 19;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 1 && j == 3)
                {
                    if (classes >= 20)
                    {
                        result[i, j] = 20;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 1 && j == 4)
                {
                    if (classes >= 12)
                    {
                        result[i, j] = 12;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 1 && j == 5)
                {
                    if (classes >= 3)
                    {
                        result[i, j] = 3;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 2 && j == 0)
                {
                    if (classes >= 10)
                    {
                        result[i, j] = 10;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 2 && j == 1)
                {
                    if (classes >= 14)
                    {
                        result[i, j] = 14;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 2 && j == 2)
                {
                    if (classes >= 22)
                    {
                        result[i, j] = 22;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 2 && j == 3)
                {
                    if (classes >= 13)
                    {
                        result[i, j] = 13;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 2 && j == 4)
                {
                    if (classes >= 8)
                    {
                        result[i, j] = 8;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 2 && j == 5)
                {
                    if (classes >= 24)
                    {
                        result[i, j] = 24;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 3 && j == 0)
                {
                    if (classes >= 5)
                    {
                        result[i, j] = 5;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 3 && j == 1)
                {
                    if (classes >= 9)
                    {
                        result[i, j] = 9;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 3 && j == 2)
                {
                    if (classes >= 23)
                    {
                        result[i, j] = 23;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 3 && j == 3)
                {
                    if (classes >= 2)
                    {
                        result[i, j] = 2;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 3 && j == 4)
                {
                    if (classes >= 6)
                    {
                        result[i, j] = 6;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 3 && j == 5)
                {
                    if (classes >= 7)
                    {
                        result[i, j] = 7;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        flag = true;
        return result;
    }
    // Second sub
    static int[,] softSecond1(int[,] a, int classes)
    {
        int rows = 4;
        int cols = 6;
        bool hasUpdatedClas = false;
        bool hasUpdatedClas2 = false;
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {

            int clas = 1;
            for (int j = 0; j < cols; j++)
            {
                if (clas == 3 && i == 1)
                {
                    if (classes == 24)
                    {
                        result[i, j] = 24;
                        clas++;
                    }
                    else
                    {
                        result[i, j] = 0;
                        clas++;
                    }

                }
                else if (i == 3 && j == 1)
                {
                    if (classes >= 3)
                    {
                        result[i, j] = 3;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 0 && classes > 6)
                {
                    result[i, j] = clas + 6;
                    if ((clas + 6) > classes)
                    {
                        result[i, j] = 0;
                    }
                    clas++;
                }
                else if (i == 1)
                {
                    result[i, j] = clas;
                    if (clas > classes)
                    {
                        result[i, j] = 0;
                    }
                    clas++;

                }
                else if (i == 2)
                {
                    if (!hasUpdatedClas)
                    {
                        clas += 17;
                        hasUpdatedClas = true;
                    }
                    result[i, j] = clas;
                    if (clas > classes)
                    {
                        result[i, j] = 0;
                    }

                    clas++;

                }
                else if (i == 3 && classes > 12)
                {
                    if (clas > classes)
                    {
                        result[i, j] = 0;
                    }
                    else
                    {
                        if (!hasUpdatedClas2)
                        {
                            clas += 12;
                            hasUpdatedClas2 = true;
                        }

                        result[i, j] = clas;

                        clas++;

                    }
                }

                else
                {
                    result[i, j] = 0;
                }
            }
        }
        return result;
    }
    static int[,] softSecond2(int[,] a, int classes)
    {
        int rows = 4;
        int cols = 6;
        int clas = 1;
        int[,] result = new int[rows, cols];


        for (int i = 0; i < rows; i++)
        {
            for (int j = cols - 1; j >= 0; j--)
            {
                if (i + j == 4)
                {
                    result[i, j] = clas;
                    IsClassMoreThanClasses(result, classes, i, j);
                    clas++;
                }
                else if (i == 2 && j == 0)
                {
                    result[i, j] = 5;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if ((i == 1 && j == 4) || (i == 2 && j == 3) || (i == 3 && j == 2))
                {
                    result[i, j] = clas + 4;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 1)
                {
                    result[i, j] = 9;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 0)
                {
                    result[i, j] = 10;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 5)
                {
                    result[i, j] = 11;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 4)
                {
                    result[i, j] = 12;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 3)
                {
                    result[i, j] = 13;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 1)
                {
                    result[i, j] = 14;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 5)
                {
                    result[i, j] = 15;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 2)
                {
                    result[i, j] = 16;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 2)
                {
                    result[i, j] = 17;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 1)
                {
                    result[i, j] = 18;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 0)
                {
                    result[i, j] = 19;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 5)
                {
                    result[i, j] = 20;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 0)
                {
                    result[i, j] = 21;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 3)
                {
                    result[i, j] = 22;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 4)
                {
                    result[i, j] = 23;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 5)
                {
                    result[i, j] = 24;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        return result;
    }
    static int[,] softSecond3(int[,] a, int classes, bool flag)
    {
        int rows = 4;
        int cols = 6;
        int clas = 1;
        int[,] result = new int[rows, cols];


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i == 0 && j == 3)
                {
                    result[i, j] = 1;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 2)
                {
                    result[i, j] = 2;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 0)
                {
                    result[i, j] = 3;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 5)
                {
                    result[i, j] = 4;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 5)
                {
                    result[i, j] = 5;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 1)
                {
                    result[i, j] = 6;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 0)
                {
                    result[i, j] = 7;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 1)
                {
                    result[i, j] = 8;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 4)
                {
                    result[i, j] = 9;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 5)
                {
                    result[i, j] = 10;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 0)
                {
                    result[i, j] = 11;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 1)
                {
                    result[i, j] = 12;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 2)
                {
                    result[i, j] = 13;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 4)
                {
                    result[i, j] = 14;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 1)
                {
                    result[i, j] = 15;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 5)
                {
                    result[i, j] = 16;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 4)
                {
                    result[i, j] = 17;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 4)
                {
                    result[i, j] = 18;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 3)
                {
                    result[i, j] = 19;
                    IsClassMoreThanClasses(result, classes, i, j);

                }
                else if (i == 1 && j == 2)
                {
                    result[i, j] = 20;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 2)
                {
                    result[i, j] = 21;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 3)
                {
                    result[i, j] = 22;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 3)
                {
                    result[i, j] = 23;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 0)
                {
                    result[i, j] = 24;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        flag = true;
        return result;
    }
    // Third sub
    static int[,] softThird1(int[,] a, int classes)
    {
        int rows = 4;
        int cols = 6;
        int clas = 1;
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = cols - 1; j >= 0; j--)
            {
                if (clas == 3)
                {
                    if (classes == 24)
                    {
                        result[i, j] = 24;
                        clas++;
                    }
                    else
                    {
                        result[i, j] = 0;
                        clas++;
                    }
                }
                else if (i == 2 && j == 4)
                {
                    if (classes >= 3)
                    {
                        result[i, j] = 3;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }

                }
                else if (clas <= classes)
                {
                    result[i, j] = clas;
                    clas++;

                }

                else
                {
                    result[i, j] = 0;
                }

            }
        }
        a = result;
        return a;
    }
    static int[,] softThird2(int[,] a, int classes)
    {
        int rows = 4;
        int cols = 6;
        int clas = 1;
        int[,] result = new int[rows, cols];


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if ((i == 1 && j == 1) || (i == 2 && j == 2) || (i == 3 && j == 3))
                {
                    result[i, j] = clas;
                    IsClassMoreThanClasses(result, classes, i, j);
                    clas++;
                }
                else if (i == 2 && j == 4)
                {
                    result[i, j] = 4;
                    IsClassMoreThanClasses(result, classes, i, j);
                }

                else if (i == 1 && j == 5)
                {
                    result[i, j] = 5;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if ((i == 2 && j == 1) || (i == 3 && j == 2))
                {
                    result[i, j] = clas + 4;
                    IsClassMoreThanClasses(result, classes, i, j);

                }
                else if ((i == 2 && j == 3))
                {

                    result[i, j] = 8;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 4)
                {
                    result[i, j] = 9;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 5)
                {
                    result[i, j] = 10;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 0)
                {
                    result[i, j] = 11;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 1)
                {
                    result[i, j] = 12;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 2)
                {
                    result[i, j] = 13;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 1)
                {
                    result[i, j] = 14;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 0)
                {
                    result[i, j] = 15;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 2)
                {
                    result[i, j] = 16;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 3)
                {
                    result[i, j] = 17;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 3)
                {
                    result[i, j] = 18;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 5)
                {
                    result[i, j] = 19;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 4)
                {
                    result[i, j] = 20;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 5)
                {
                    result[i, j] = 21;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 0)
                {
                    result[i, j] = 22;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 0)
                {
                    result[i, j] = 23;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 4)
                {
                    result[i, j] = 24;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        return result;
        

    }
    static int[,] softThird3(int[,] a, int classes, bool flag)
    {
        int rows = 4;
        int cols = 6;
        int clas = 1;
        int[,] result = new int[rows, cols];


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i == 2 && j == 3)
                {
                    result[i, j] = 1;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 0)
                {
                    result[i, j] = 2;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 5)
                {
                    result[i, j] = 3;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 5)
                {
                    result[i, j] = 4;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 2)
                {
                    result[i, j] = 5;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 3)
                {
                    result[i, j] = 6;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 1)
                {
                    result[i, j] = 7;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 0)
                {
                    result[i, j] = 8;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 0)
                {
                    result[i, j] = 9;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 4)
                {
                    result[i, j] = 10;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 2)
                {
                    result[i, j] = 11;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 3)
                {
                    result[i, j] = 12;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 1)
                {
                    result[i, j] = 13;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 5)
                {
                    result[i, j] = 14;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 1)
                {
                    result[i, j] = 15;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 5)
                {
                    result[i, j] = 16;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 4)
                {
                    result[i, j] = 17;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 2)
                {
                    result[i, j] = 18;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 3)
                {
                    result[i, j] = 19;
                    IsClassMoreThanClasses(result, classes, i, j);

                }
                else if (i == 3 && j == 0)
                {
                    result[i, j] = 20;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 1)
                {
                    result[i, j] = 21;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 4)
                {
                    result[i, j] = 22;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 4)
                {
                    result[i, j] = 23;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 2)
                {
                    result[i, j] = 24;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        flag = true;
        return result;
    }
    // Four sub
    static int[,] softFour1(int[,] a, int classes)
    {
        int rows = 4;
        int cols = 6;
        bool hasUpdatedClas = false;
        bool hasUpdatedClas2 = false;
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {

            int clas = 1;
            for (int j = cols - 1; j >= 0; j--)
            {
                if (clas == 3 && i == 1)
                {
                    if (classes == 24)
                    {
                        result[i, j] = 24;
                        clas++;
                    }
                    else
                    {
                        result[i, j] = 0;
                        clas++;
                    }

                }
                else if (i == 3 && j == 4)
                {
                    if (classes >= 3)
                    {
                        result[i, j] = 3;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
                else if (i == 0 && classes > 6)
                {
                    result[i, j] = clas + 6;
                    if ((clas + 6) > classes)
                    {
                        result[i, j] = 0;
                    }
                    clas++;
                }
                else if (i == 1)
                {
                    result[i, j] = clas;
                    if (clas > classes)
                    {
                        result[i, j] = 0;
                    }
                    clas++;

                }
                else if (i == 2)
                {
                    if (!hasUpdatedClas)
                    {
                        clas += 17;
                        hasUpdatedClas = true;
                    }
                    result[i, j] = clas;
                    if (clas > classes)
                    {
                        result[i, j] = 0;
                    }

                    clas++;

                }
                else if (i == 3 && classes > 12)
                {
                    if (clas > classes)
                    {
                        result[i, j] = 0;
                    }
                    else
                    {
                        if (!hasUpdatedClas2)
                        {
                            clas += 12;
                            hasUpdatedClas2 = true;
                        }

                        result[i, j] = clas;

                        clas++;

                    }
                }

                else
                {
                    result[i, j] = 0;
                }
            }
        }
        return result;
    }
    static int[,] softFour2(int[,] a, int classes)
    {
        int rows = 4;
        int cols = 6;
        int clas = 1;
        int[,] result = new int[rows, cols];


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (j == i + 1)
                {
                    result[i, j] = clas;
                    IsClassMoreThanClasses(result, classes, i, j);
                    clas++;
                }
                else if (i == 2 && j == 5)
                {
                    result[i, j] = 5;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if ((i == 1 && j == 1) || (i == 2 && j == 2) || (i == 3 && j == 3))
                {
                    result[i, j] = clas + 4;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 4)
                {
                    result[i, j] = 9;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 5)
                {
                    result[i, j] = 10;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 0)
                {
                    result[i, j] = 11;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 1)
                {
                    result[i, j] = 12;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 2)
                {
                    result[i, j] = 13;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 4)
                {
                    result[i, j] = 14;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 0)
                {
                    result[i, j] = 15;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 3)
                {
                    result[i, j] = 16;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 3)
                {
                    result[i, j] = 17;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 4)
                {
                    result[i, j] = 18;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 5)
                {
                    result[i, j] = 19;
                    IsClassMoreThanClasses(result, classes, i, j);

                }
                else if (i == 0 && j == 0)
                {
                    result[i, j] = 20;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 0)
                {
                    result[i, j] = 21;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 2)
                {
                    result[i, j] = 22;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 5)
                {
                    result[i, j] = 23;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 1)
                {
                    result[i, j] = 24;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        return result;
        
    }
    static int[,] softFour3(int[,] a, int classes, bool flag)
    {
        int rows = 4;
        int cols = 6;
        int clas = 1;
        int[,] result = new int[rows, cols];


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i == 2 && j == 2)
                {
                    result[i, j] = 1;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 5)
                {
                    result[i, j] = 2;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 0)
                {
                    result[i, j] = 3;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 0)
                {
                    result[i, j] = 4;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 3)
                {
                    result[i, j] = 5;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 2)
                {
                    result[i, j] = 6;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 4)
                {
                    result[i, j] = 7;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 5)
                {
                    result[i, j] = 8;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 5)
                {
                    result[i, j] = 9;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 1)
                {
                    result[i, j] = 10;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 3)
                {
                    result[i, j] = 11;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 2)
                {
                    result[i, j] = 12;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 4)
                {
                    result[i, j] = 13;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 0)
                {
                    result[i, j] = 14;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 4)
                {
                    result[i, j] = 15;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 0)
                {
                    result[i, j] = 16;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 1)
                {
                    result[i, j] = 17;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 2 && j == 3)
                {
                    result[i, j] = 18;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 2)
                {
                    result[i, j] = 19;
                    IsClassMoreThanClasses(result, classes, i, j);

                }
                else if (i == 3 && j == 5)
                {
                    result[i, j] = 20;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 4)
                {
                    result[i, j] = 21;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 0 && j == 1)
                {
                    result[i, j] = 22;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 1 && j == 1)
                {
                    result[i, j] = 23;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else if (i == 3 && j == 3)
                {
                    result[i, j] = 24;
                    IsClassMoreThanClasses(result, classes, i, j);
                }
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        flag = true;
        return result;
    }

    static void fillRooms(List<string[,]> timeTableForTotalClas, List<room> listRooms, int totalClass)
    {
        int rows = 4;
        int cols = 6;

        for (int i = 0; i < rows; i++)
        {
            for  (int j = 0; j < cols; j++)
            {
                for(int clas = 0; clas < totalClass; clas++)
                {
                    if (!string.IsNullOrEmpty(timeTableForTotalClas[clas][i,j]))
                    {
                        timeTableForTotalClas[clas][i, j] += " " + listRooms[clas].roomName; ;
                    }
                }
            }
            
        }

    }
    static void fillSubForEachClass(int[,] timeTableForEchSub, string[,] timeTableForEachClas, string subName, int clas)
    {
        int rows = 4;
        int cols = 6;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (timeTableForEchSub[i, j] == clas)
                {
                    timeTableForEachClas[i, j] = subName;
                }
            }
        }
    }
    static void softTimeTable(List<subjectCacul> listSubject, List<int[,]> timeTableForTotalSub, List<string[,]> timeTableForTotalClas, int totalClass, bool flag1, bool flag2,bool flag3, bool flag4,List<room> Room)
    {
        int rows = 4;
        int cols = 6;

        int[,] TimeTbForEarchSub = new int[rows, cols];
        int totalSubject = listSubject.Count;

        for (int cls = 0; cls < totalClass; cls++)
        {
            string[,] timeTableForEachClas = new string[rows, cols];
            for (int i = 0; i < totalSubject; i++)
            {
                if (i == 0)
                {
                    if (listSubject[i].appear == 1)
                    {
                        TimeTbForEarchSub = softFirst1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }
                    else if (listSubject[i].appear == 2)
                    {
                        TimeTbForEarchSub = softFirst1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softFirst2(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);

                    }
                    else if (listSubject[i].appear == 3)
                    {
                        TimeTbForEarchSub = softFirst1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softFirst2(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softFirst3(TimeTbForEarchSub, totalClass,flag1);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }

                }
                else if (i == 1)
                {
                    if (listSubject[i].appear == 1)
                    {
                        TimeTbForEarchSub = softSecond1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }
                    else if (listSubject[i].appear == 2)
                    {
                        TimeTbForEarchSub = softSecond1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softSecond2(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);

                    }
                    else if (listSubject[i].appear == 3)
                    {
                        TimeTbForEarchSub = softSecond1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softSecond2(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softSecond3(TimeTbForEarchSub, totalClass, flag2);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }
                }
                else if (i == 2)
                {
                    if (listSubject[i].appear == 1)
                    {
                        TimeTbForEarchSub = softThird1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }
                    else if (listSubject[i].appear == 2)
                    {
                        TimeTbForEarchSub = softThird1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softThird2(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }
                    else if (listSubject[i].appear == 3)
                    {
                        TimeTbForEarchSub = softThird1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softThird2(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softThird3(TimeTbForEarchSub, totalClass, flag3);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }
                }
                else if (i == 3)
                {
                    if (listSubject[i].appear == 1)
                    {
                        TimeTbForEarchSub = softFour1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }
                    else if (listSubject[i].appear == 2)
                    {
                        TimeTbForEarchSub = softFour1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softFour2(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }
                    else if (listSubject[i].appear == 3)
                    {
                        TimeTbForEarchSub = softFour1(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softFour2(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        TimeTbForEarchSub = softFour3(TimeTbForEarchSub, totalClass, flag4);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }
                }
                else if(i == 4)
                {
                    if (listSubject[i].appear == 1)
                    {
                        if(flag1 == false)
                        {
                            TimeTbForEarchSub = softFirst3(TimeTbForEarchSub, totalClass,flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                        else if(flag2 == false)
                        {
                            TimeTbForEarchSub = softSecond3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                        else if (flag3 == false)
                        {
                            TimeTbForEarchSub = softThird3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                        else if (flag4 == false)
                        {
                            TimeTbForEarchSub = softFour3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                    }
                    
                    else if (listSubject[i].appear == 2)
                    {
                        if (flag1 == false && flag2 == false)
                        {
                            TimeTbForEarchSub = softFirst3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softSecond3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                        else if (flag1 == false && flag3 == false)
                        {
                            TimeTbForEarchSub = softFirst3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softSecond3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                        else if (flag1 == false && flag4 == false)
                        {
                            TimeTbForEarchSub = softFirst3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softFour3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                        else if (flag2 == false && flag3 == false)
                        {
                            TimeTbForEarchSub = softSecond3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softThird3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                        else if (flag2 == false && flag4 == false)
                        {
                            TimeTbForEarchSub = softSecond3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softFour3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                        else if (flag3 == false && flag4 == false)
                        {
                            TimeTbForEarchSub = softThird3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softFour3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                    }
                    else if (listSubject[i].appear == 3)
                    {
                        if (flag1 == false && flag2 == false && flag3 == false)
                        {
                            TimeTbForEarchSub = softFirst3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softSecond3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softThird3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                        else if (flag1 == false && flag3 == false && flag4 == false)
                        {
                            TimeTbForEarchSub = softFirst3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softThird3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softFour3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                        else if (flag2 == false && flag3 == false && flag4 == false)
                        {
                            TimeTbForEarchSub = softSecond3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softThird3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                            TimeTbForEarchSub = softFour3(TimeTbForEarchSub, totalClass, flag1);
                            fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        }
                    }
                }
            }
            timeTableForTotalClas.Add(timeTableForEachClas);
        }
        //fillRooms(timeTableForTotalClas, Room, totalClass);
    }
    // --------------------------------
    // print table
    static void PrintTable(List<int[,]> timeTableForTotalSub, int rows, int columns)
    {
        for (int ob = 0; ob < timeTableForTotalSub.Count; ob++)
        {
            int[,] currentTable = timeTableForTotalSub[ob];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("|" + currentTable[i, j] + "\t");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine(new string('-', columns * 8));
        }
    }
    static void PrintTableForSub(List<string[,]> timeTableForTotalSub, int rows, int columns)
    {
        Random random = new Random();
        for (int ob = 0; ob < timeTableForTotalSub.Count; ob++)
        {
            string[,] currentTable = timeTableForTotalSub[ob];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("|" + currentTable[i, j] + "\t");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine(new string('-', columns * 8));
        }
    }
    static void PrintRandomTableForSub(List<string[,]> timeTableForTotalSub, int rows, int columns)
    {
        Random random = new Random();
        List<int> usedIndices = new List<int>();

        for (int ob = 0; ob < timeTableForTotalSub.Count; ob++)
        {
            int randomIndex;
            do
            {
                randomIndex = random.Next(0, timeTableForTotalSub.Count);
            } while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);

            string[,] currentTable = timeTableForTotalSub[randomIndex];
            Console.WriteLine("------------------");
            Console.WriteLine("IT " + (ob + 1));
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("|" + currentTable[i, j] + "\t");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine(new string('-', columns * 8));
        }
    }

    static void Main(string[] args)
    {
        int row = 4;
        int col = 6;
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        List<subject> subjects = new List<subject>();
        List<subjectCacul> subjectsCacul = new List<subjectCacul>();
        List<checkdays> checkDaysInWeek = new List<checkdays>();
        List<room> listRooms = new List<room>();


        for (int i = 0; i < row; i++)
        {
            checkdays check = new checkdays();
            check.ca1 = true; check.ca2 = true; check.ca3 = true; check.ca4 = true;
            checkDaysInWeek.Add(check);
        }

        Console.Write("Nhap so luong mon hoc: ");
        int totalSubjects = int.Parse(Console.ReadLine());
        Console.Write("Nhap so luong lop hoc: ");
        int totalClass = int.Parse(Console.ReadLine());

        for (int i = 0; i < totalClass; i++)
        {
            room Room = new room();
            Room.roomName = $"DTD-70{i}";
            listRooms.Add(Room);
        }

        for (int i = 0; i < totalSubjects; i++)
        {
            subject MH = new subject();
            Console.WriteLine("Nhap ten mon hoc: ");
            MH.subjectName = Console.ReadLine();
            Console.WriteLine("Nhap ngay bat dau: ");
            MH.dateStart = Console.ReadLine();
            Console.WriteLine("Nhap ngay ket thuc: ");
            MH.dateEnd = Console.ReadLine();
            Console.WriteLine("Nhap tin chi: ");
            MH.credit = Convert.ToInt32(Console.ReadLine());
            subjects.Add(MH);
        }

        for (int i = 0; i < totalSubjects; i++)
        {
            subjectCacul sbcacul = new subjectCacul();
            sbcacul = caculateSubject(subjects[i]);
            subjectsCacul.Add(sbcacul);
        }

        List<int[,]> timeTableForTotalSub = new List<int[,]>();
        List<string[,]> timeTableForTotalClas = new List<string[,]>();
        softTimeTable(subjectsCacul, timeTableForTotalSub, timeTableForTotalClas, totalClass,flag1,flag2,flag3,flag4, listRooms);
        //PrintTable(timeTableForTotalSub, row, col);
        PrintRandomTableForSub(timeTableForTotalClas, row, col);
        //PrintTableForSub(timeTableForTotalClas, row, col);
    }
}