import numpy as np
from matplotlib.pyplot import plot, ylim, show, vlines, text
from fuzzylab import linspace, trapmf, defuzz

def koef(value,a,b,c,d):  
    if value <= a:
        return 0
    if a <= value and value <= b:
        return (value-a)/(b-a)
    if b <= value and value <= c:
        return 1
    if c <= value and value <= d:
        return (d - value)/(d - c)
    if d <= value:  
        return 0

x_plot = linspace(0, 1, 50)
# Трапеции для уровня жидкости
print("Уровень жидкости")
print("Z-линейная")
tramp1_3 = int(input())
tramp1_4 = int(input())

level_min = trapmf(x_plot,[0, 0, tramp1_3, tramp1_4])

print("Трапецивидная")
tramp2_1 = int(input())
tramp2_2 = int(input())
tramp2_3 = int(input())
tramp2_4 = int(input())

level_mid = trapmf(x_plot,[tramp2_1, tramp2_2, tramp2_3, tramp2_4])

print("S-линейная")
tramp3_1 = int(input())
tramp3_2 = int(input())

level_larg = trapmf(x_plot,[tramp3_1, tramp3_2, 50, 50])


print("Текущий уровень")
value1 = float(input())
tramp1_koef_level = koef(value1,0,0,tramp1_3,tramp1_4)
tramp2_koef_level = koef(value1,tramp2_1,tramp2_2,tramp2_3,tramp2_4)
tramp3_koef_level = koef(value1,tramp3_1,tramp3_2,50,50)

print("Степени истенности уровня жидкости")
print(tramp1_koef_level)
print(tramp2_koef_level)
print(tramp3_koef_level)


#Трапеции для расхода жидкости
print("Расход жидкости")
print("Z-линейная")
tramp1_3 = int(input())
tramp1_4 = int(input())

consumption_min = trapmf(x_plot,[0, 0, tramp1_3, tramp1_4])

print("Трапецивидная")
tramp2_1 = int(input())
tramp2_2 = int(input())
tramp2_3 = int(input())
tramp2_4 = int(input())

consumption_mid = trapmf(x_plot,[tramp2_1, tramp2_2, tramp2_3, tramp2_4])

print("S-линейная")
tramp3_1 = int(input())
tramp3_2 = int(input())

consumption_larg = trapmf(x_plot,[tramp3_1, tramp3_2, 50, 50])


print("Текущий расход")
value1 = float(input())
tramp1_koef_consumption = koef(value1,0,0,tramp1_3,tramp1_4)
tramp2_koef_consumption = koef(value1,tramp2_1,tramp2_2,tramp2_3,tramp2_4)
tramp3_koef_consumption = koef(value1,tramp3_1,tramp3_2,50,50)

print("Степени истенности расхода жидкости")
print(tramp1_koef_consumption)
print(tramp2_koef_consumption)
print(tramp3_koef_consumption)

switcher = {
        1: "Большой",
        2: "Средний",
        3: "Малый"
    }

list_koncendent=[]
for x in range(9):
  print("Выберите: 1-Большой, 2-Средний, 3-Малый")
  a = int(input())
  if switcher.get(a, "false") != "false":
      list_koncendent.append(a)

list_of_weigh=[]
for x in range(9):
  print("Введите коэфициент")
  a = float(input())
  list_of_weigh.append(a)

list_of_truth=[]

list_of_truth.append(np.minimum(tramp1_koef_level,tramp3_koef_consumption))
list_of_truth.append(np.minimum(tramp1_koef_level,tramp2_koef_consumption))
list_of_truth.append(np.minimum(tramp1_koef_level,tramp1_koef_consumption))
list_of_truth.append(np.minimum(tramp2_koef_level,tramp3_koef_consumption))
list_of_truth.append(np.minimum(tramp2_koef_level,tramp2_koef_consumption))
list_of_truth.append(np.minimum(tramp2_koef_level,tramp1_koef_consumption))
list_of_truth.append(np.minimum(tramp3_koef_level,tramp3_koef_consumption))
list_of_truth.append(np.minimum(tramp3_koef_level,tramp2_koef_consumption))
list_of_truth.append(np.minimum(tramp3_koef_level,tramp1_koef_consumption))
#print(list_of_truth)
max_large = 0
max_middle = 0
max_small = 0
for x in range(len(list_koncendent)):
  if(list_koncendent[x] == 1):
    max_large = np.maximum(max_large,list_of_truth[x]*list_of_weigh[x])
  if(list_koncendent[x] == 2):
    max_middle = np.maximum(max_middle,list_of_truth[x]*list_of_weigh[x])
  if(list_koncendent[x] == 3):
    max_small = np.maximum(max_small,list_of_truth[x]*list_of_weigh[x])


answer = np.maximum(max_middle*consumption_mid, np.maximum( max_small*consumption_min, max_large * consumption_larg))

x1 = defuzz(x_plot, answer, 'centroid')
print("Ответ: ")
print(x1)
vlines(x1, -0.2, 1.2, color='k')
text(x1, 0.27, 'centroid', fontweight='bold')

plot(x_plot,answer, lineWidth=3)
ylim(0, 1)
show()