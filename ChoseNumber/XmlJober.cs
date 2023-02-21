using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace ChoseNumber
{
    public class XmlJober
    {
        // имя файла
        private static string fileName = "score.xml";
        // вывод информации в файл о пройденной игре
        public static void AddDataToFile(UserScore score)
        {
            // если файла нет, создаем новый
            if (!File.Exists(fileName))
            {
                XmlDocument doc = new XmlDocument();
                XmlNode rootNode = doc.CreateElement("rdf:RDF");
                doc.AppendChild(rootNode);
                doc.Save(fileName);
            }
            // создаем док
            XmlDocument xDoc = new XmlDocument();
            // читаем в него все содержимое
            xDoc.Load(fileName);
            // получаем рут элемент
            XmlElement? xRoot = xDoc.DocumentElement;
            // записываем информацию о пользователе
            XmlElement personElem = xDoc.CreateElement("person");
            XmlAttribute nameAttr = xDoc.CreateAttribute("name");
            XmlElement sexElem = xDoc.CreateElement("gender");
            XmlElement ageElem = xDoc.CreateElement("age");
            XmlText nameText = xDoc.CreateTextNode(score.UserName);
            XmlText sexText = xDoc.CreateTextNode(score.Sex);
            XmlText ageText = xDoc.CreateTextNode(score.Age.ToString());
            // выводи информацию в док
            nameAttr.AppendChild(nameText);
            sexElem.AppendChild(sexText);
            ageElem.AppendChild(ageText);
            personElem.Attributes.Append(nameAttr);
            personElem.AppendChild(sexElem);
            personElem.AppendChild(ageElem);
            // записываем информацию о результатах
            XmlElement scoresElem = xDoc.CreateElement("scores");
            for (int i = 0; i < 5; i++)
            {
                XmlAttribute timeAttr = xDoc.CreateAttribute("time");
                XmlElement scoreElem = xDoc.CreateElement("score");
                XmlText timeText = xDoc.CreateTextNode(score.Time[i].ToString());
                XmlText scoreText = xDoc.CreateTextNode(score.Score[i].ToString());
                timeAttr.AppendChild(timeText);
                scoreElem.AppendChild(scoreText);
                scoreElem.Attributes.Append(timeAttr);
                scoresElem.AppendChild(scoreElem);
            }
            XmlAttribute alltimeAttr = xDoc.CreateAttribute("time");
            XmlElement allscoreElem = xDoc.CreateElement("allscore");
            XmlText alltimeText = xDoc.CreateTextNode(score.AllTime().ToString());
            XmlText allscoreText = xDoc.CreateTextNode(score.AllScore().ToString());
            // выводим в док инфомрацию о результатах
            alltimeAttr.AppendChild(alltimeText);
            allscoreElem.AppendChild(allscoreText);
            allscoreElem.Attributes.Append(alltimeAttr);
            personElem.AppendChild(scoresElem);
            personElem.AppendChild(allscoreElem);
            // добавляем в рут полученную информацию
            xRoot?.AppendChild(personElem);
            // сохраняем файл
            xDoc.Save(fileName);
        }
        public static List<UserScore> ReadAllData()
        {
            List<UserScore> data = new List<UserScore>();
            // если файла нет, создаем его
            if (!File.Exists(fileName))
            {
                XmlDocument doc = new XmlDocument();
                XmlNode rootNode = doc.CreateElement("rdf:RDF");
                doc.AppendChild(rootNode);
                doc.Save(fileName);
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            // получим корневой элемент
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {
                    // получаем атрибут name
                    XmlNode? attr = xnode.Attributes.GetNamedItem("name");
                    string name = attr.Value;
                    int age = 0;
                    Gender gender = 0;
                    int[] scores = new int[5];
                    long[] times = new long[5];
                    int i = 0;
                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        // если узел age
                        if (childnode.Name == "age")
                        {
                            age = int.Parse(childnode.InnerText);
                        }
                        // если узел gender
                        if (childnode.Name == "gender")
                        {
                            gender = childnode.InnerText == "М" ? Gender.Male : Gender.Female;
                        }
                        // если узел score
                        if (childnode.Name == "scores")
                        {
                            // обходим все дочерние узлы элемента score
                            foreach (XmlNode scoreNode in childnode.ChildNodes)
                            { 
                                scores[i]= int.Parse(scoreNode.InnerText);
                                times[i] = long.Parse(scoreNode.Attributes.GetNamedItem("time").Value);
                                i++;
                            }
                        }
                    }
                    UserScore user = new UserScore(name, gender, age);
                    user.Time = times;
                    user.Score = scores;
                    data.Add(user);
                }
            }
            // возвращаем массив данных
            return data;
        }
    }
}
