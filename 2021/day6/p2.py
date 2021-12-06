fish = {0: 0, 1:0, 2: 0, 3 :0, 4: 0, 5 :0, 6 :0, 7 :0, 8: 0}

with open('p1.in') as file:
    for fline in file:
        fisharr = fline.strip().split(",")

for f in fisharr:
    fish[int(f)] += 1

for n in range(256):
    newfish = fish[0]
    for key in range(9):
        if(key < 8):
            fish[key] = fish[key+1]
        else:
            fish[key] = newfish
            fish[6] += newfish

total = 0
for key in fish:
    total += fish[key]

print(total)