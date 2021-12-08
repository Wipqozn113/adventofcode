count = 0
with open('p1.in') as file:
    for line in file:
        output = line.strip().split("|")[1].split(" ")
        for out in output:
            if(len(out) in [2, 4, 3, 7]):
                count += 1

print(count)


    