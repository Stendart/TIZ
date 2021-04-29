using System;
using chen0040.ExpertSystem;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        public static RuleInferenceEngine getInferenceEngine()
        {
            RuleInferenceEngine rie = new RuleInferenceEngine();

            Rule rule = new Rule("Волнистый");
            rule.AddAntecedent(new IsClause("Жилплощадь маленькая", "да"));
            rule.AddAntecedent(new IsClause("Доход низкий", "да"));
            rule.setConsequent(new IsClause("вид", "Волнистый"));
            rie.AddRule(rule);

            rule = new Rule("Корелла");
            rule.AddAntecedent(new IsClause("Жилплощадь маленькая", "да"));
            rule.AddAntecedent(new IsClause("Доход низкий", "нет"));
            rule.setConsequent(new IsClause("вид", "Корелла"));
            rie.AddRule(rule);

            rule = new Rule("Неразлучник");
            rule.AddAntecedent(new IsClause("Жилплощадь маленькая", "нет"));
            rule.AddAntecedent(new IsClause("Доход низкий", "да"));
            rule.AddAntecedent(new IsClause("Планируется покупка пары", "да"));
            rule.setConsequent(new IsClause("вид", "Неразлучник"));
            rie.AddRule(rule);

            rule = new Rule("Волнистый");
            rule.AddAntecedent(new IsClause("Жилплощадь маленькая", "нет"));
            rule.AddAntecedent(new IsClause("Доход низкий", "да"));
            rule.AddAntecedent(new IsClause("Планируется покупка пары", "нет"));
            rule.setConsequent(new IsClause("вид", "Волнистый"));
            rie.AddRule(rule);

            rule = new Rule("Не нужен попугай");
            rule.AddAntecedent(new IsClause("Жилплощадь маленькая", "нет"));
            rule.AddAntecedent(new IsClause("Доход низкий", "нет"));
            rule.AddAntecedent(new IsClause("Характер злой", "да"));
            rule.setConsequent(new IsClause("вид", "Не нужен вам попугай!"));
            rie.AddRule(rule);

            rule = new Rule("Амазон");
            rule.AddAntecedent(new IsClause("Жилплощадь маленькая", "нет"));
            rule.AddAntecedent(new IsClause("Доход низкий", "нет"));
            rule.AddAntecedent(new IsClause("Характер злой", "нет"));
            rule.AddAntecedent(new IsClause("Готовы ли терпеть запах", "да"));
            rule.setConsequent(new IsClause("вид", "Амазон"));
            rie.AddRule(rule);

            rule = new Rule("Эклектус");
            rule.AddAntecedent(new IsClause("Жилплощадь маленькая", "нет"));
            rule.AddAntecedent(new IsClause("Доход низкий", "нет"));
            rule.AddAntecedent(new IsClause("Характер злой", "нет"));
            rule.AddAntecedent(new IsClause("Готовы ли терпеть запах", "нет"));
            rule.AddAntecedent(new IsClause("Вам нужен собеседник", "нет"));
            rule.setConsequent(new IsClause("вид", "Эклектус"));
            rie.AddRule(rule);

            rule = new Rule("Жако");
            rule.AddAntecedent(new IsClause("Жилплощадь маленькая", "нет"));
            rule.AddAntecedent(new IsClause("Доход низкий", "нет"));
            rule.AddAntecedent(new IsClause("Характер злой", "нет"));
            rule.AddAntecedent(new IsClause("Готовы ли терпеть запах", "нет"));
            rule.AddAntecedent(new IsClause("Вам нужен собеседник", "да"));
            rule.AddAntecedent(new IsClause("Готовы к сложному уходу", "да"));
            rule.setConsequent(new IsClause("вид", "Жако"));
            rie.AddRule(rule);

            rule = new Rule("Какаду");
            rule.AddAntecedent(new IsClause("Жилплощадь маленькая", "нет"));
            rule.AddAntecedent(new IsClause("Доход низкий", "нет"));
            rule.AddAntecedent(new IsClause("Характер злой", "нет"));
            rule.AddAntecedent(new IsClause("Готовы ли терпеть запах", "нет"));
            rule.AddAntecedent(new IsClause("Вам нужен собеседник", "да"));
            rule.AddAntecedent(new IsClause("Готовы к сложному уходу", "нет"));
            rule.AddAntecedent(new IsClause("Готовы уделять много времени птице", "нет"));
            rule.setConsequent(new IsClause("вид", "Какаду"));
            rie.AddRule(rule);

            rule = new Rule("Ара");
            rule.AddAntecedent(new IsClause("Жилплощадь маленькая", "нет"));
            rule.AddAntecedent(new IsClause("Доход низкий", "нет"));
            rule.AddAntecedent(new IsClause("Характер злой", "нет"));
            rule.AddAntecedent(new IsClause("Готовы ли терпеть запах", "нет"));
            rule.AddAntecedent(new IsClause("Вам нужен собеседник", "да"));
            rule.AddAntecedent(new IsClause("Готовы к сложному уходу", "нет"));
            rule.AddAntecedent(new IsClause("Готовы уделять много времени птице", "да"));
            rule.setConsequent(new IsClause("вид", "Ара"));
            rie.AddRule(rule);

            return rie;
        }

        static void Main(string[] args)
        {
            RuleInferenceEngine rie = getInferenceEngine();

            Console.WriteLine("Давайте выберем вам птицу!");
            rie.ClearFacts();

            List<Clause> unproved_conditions = new List<Clause>();

            Clause conclusion = null;
            while (conclusion == null)
            {
                conclusion = rie.Infer("вид", unproved_conditions);
                if (conclusion == null)
                {
                    if (unproved_conditions.Count == 0)
                    {
                        break;
                    }
                    Clause c = unproved_conditions[0];
                    Console.WriteLine("Вопрос: " + c + "?");
                    unproved_conditions.Clear();
                    //Console.WriteLine(c.Variable + "?");
                    String value = Console.ReadLine();
                    rie.AddFact(new IsClause(c.Variable, value));
                }
            }

            Console.WriteLine("Заключение: " + conclusion);
            Console.WriteLine("{0}", rie.Facts);


            Console.ReadKey();
        }
    }
}
