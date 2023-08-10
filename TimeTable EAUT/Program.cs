
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
                    result[i, j] = 0;
                    clas++;
                }
                else if (clas <= classes)
                {
                    result[i, j] = clas;
                    clas++;

                }
                else if (clas == classes + 1)
                {
                    result[i, j] = 3;
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
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        return result;
    }
    static int[,] softFirst3(int[,] a, int classes)
    {
        return a;
    }
    // Second sub
    static int[,] softSecond1(int[,] a, int classes)
    {
        int rows = 4;
        int cols = 6;
        bool hasUpdatedClas = false;
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {

            int clas = 1;
            for (int j = 0; j < cols; j++)
            {
                if (clas == 3 && i == 1)
                {
                    result[i, j] = 0;
                    clas++;
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
                    if ((clas) > classes)
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
                        if (!hasUpdatedClas)
                        {
                            clas += 12;
                            hasUpdatedClas = true;
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
        a = result;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (classes == 12)
                {
                    r += 3;
                    a[r, c] = 3;
                    break;
                }
                else
                {
                    if (a[r, c] == classes)
                    {
                        c += 1;
                        a[r, c] = 3;
                        break;
                    }
                }
            }
        }
        return a;
    }
    static int[,] softSecond2(int[,] a, int classes)
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
                    if (result[i, j] > classes)
                    {
                        result[i, j] = 0;
                    }
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
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        return result;
    }
    static int[,] softSecond3(int[,] a, int classes)
    {
        return a;
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
                    result[i, j] = 0;
                    clas++;
                }
                else if (clas <= classes)
                {
                    result[i, j] = clas;
                    clas++;
                }
                else if (clas == classes + 1)
                {
                    result[i, j] = 3;
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
                else
                {
                    result[i, j] = 0;
                }
            }

        }
        return result;

    }
    static int[,] softThird3(int[,] a, int classes)
    {
        return a;
    }
    // Four sub
    static int[,] softFour1(int[,] a, int classes)
    {
        int rows = 4;
        int cols = 6;
        bool hasUpdatedClas = false;
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {

            int clas = 1;
            for (int j = cols - 1; j >= 0; j--)
            {
                if (clas == 3 && i == 1)
                {
                    result[i, j] = 0;
                    clas++;
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
                    if ((clas) > classes)
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
                        if (!hasUpdatedClas)
                        {
                            clas += 12;
                            hasUpdatedClas = true;
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
        a = result;
        for (int r = 0; r < rows; r++)
        {
            for (int c = cols - 1; c >= 0; c--)
            {
                if (classes == 12)
                {
                    r += 3;
                    a[r, c] = 3;
                    break;
                }
                else
                {
                    if (a[r, c] == classes)
                    {
                        c -= 1;
                        a[r, c] = 3;
                        break;
                    }
                }
            }
        }
        return a;
    }
    static int[,] softFour2(int[,] a, int classes)
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
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        return result;
    }
    static int[,] softFour3(int[,] a, int classes)
    {
        return a;
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
    static void softTimeTable(List<subjectCacul> listSubject, List<int[,]> timeTableForTotalSub, List<string[,]> timeTableForTotalClas, int totalClass)
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
                        TimeTbForEarchSub = softFirst3(TimeTbForEarchSub, totalClass);
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
                        TimeTbForEarchSub = softSecond3(TimeTbForEarchSub, totalClass);
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
                        TimeTbForEarchSub = softThird3(TimeTbForEarchSub, totalClass);
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
                        TimeTbForEarchSub = softFour3(TimeTbForEarchSub, totalClass);
                        fillSubForEachClass(TimeTbForEarchSub, timeTableForEachClas, listSubject[i].subjectName, cls + 1);
                        timeTableForTotalSub.Add(TimeTbForEarchSub);
                    }
                }
            }
            timeTableForTotalClas.Add(timeTableForEachClas);

        }
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
    //static void PrintTableForSub(List<string[,]> timeTableForTotalSub, int rows, int columns)
    //{
    //    Random random = new Random();
    //    for (int ob = 0; ob < timeTableForTotalSub.Count; ob++)
    //    {
    //        string[,] currentTable = timeTableForTotalSub[random.Next(0, timeTableForTotalSub.Count)];

    //        for (int i = 0; i < rows; i++)
    //        {
    //            for (int j = 0; j < columns; j++)
    //            {
    //                Console.Write("|" + currentTable[i, j] + "\t");
    //            }
    //            Console.WriteLine("|");
    //        }

    //        Console.WriteLine(new string('-', columns * 8));
    //    }
    //}
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
        List<subject> subjects = new List<subject>();
        List<subjectCacul> subjectsCacul = new List<subjectCacul>();
        List<checkdays> checkDaysInWeek = new List<checkdays>();

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
        softTimeTable(subjectsCacul, timeTableForTotalSub, timeTableForTotalClas, totalClass);
        //PrintTable(timeTableForTotalSub, row, col);
        PrintRandomTableForSub(timeTableForTotalClas, row, col);
    }
}