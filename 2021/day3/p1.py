def most_common(lst):
    return max(set(lst), key=lst.count)

lines = []
gamma = ""
epsilon = ""
with open('p1.in') as file:
    for line in file:
        l = line.strip()
        i = 0
        for c in l:
            if(len(lines) < i + 1):
                lines.append([])
            lines[i].append(c)
            i += 1

for l in lines:
    mc = most_common(l)
    gamma += mc
    lc = ("0" if mc == "1" else "1")
    epsilon += lc

g = int(gamma,2)
e = int(epsilon, 2)
print(g * e)
