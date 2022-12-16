sum_inv = []
summe = 0
max1 = 0
max2 = 0
max3 = 0
total = 0

input_txt = open("input.txt")
inventory = input_txt.readlines()

for counter in range(len(inventory)):
    if inventory[counter] == "\n":
        sum_inv.append(summe)
        summe = 0
    elif counter == (len(inventory)-1): 
        summe += int(inventory[counter])
        sum_inv.append(summe)
    else:
        summe += int(inventory[counter])
print(sum_inv)

sum_inv.sort(reverse=True)
del sum_inv[3:]
print(sum_inv)
for number in sum_inv:
    total += number
print(total)
