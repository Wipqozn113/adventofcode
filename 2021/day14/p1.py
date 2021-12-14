def Insert(template, rules):
    temp = list(template)
    n = 1
    for i in range(len(template)):
        if template[i:i+2] in rules:
            temp.insert(i+n, rules[template[i:i+2]])
            n += 1

    return ''.join(temp)

def FindMaxMin(template, characters):
    max = None
    min = None
    for char in characters:
        c = template.count(char)
        print(char, c)
        if max is None:
            max = c
            min = c
        if c > max:
            max = c
        if c < min:
            min = c

    print(max - min)

template = ""
rules = {}
characters = {}
with open('p1.in') as file:
    for line in file:
        if line.strip() == "":
            continue

        l = line.strip().split("->")
        if len(l) == 2:
            rules[l[0].strip()] = l[1].strip()
            characters[l[1].strip()] = 0
        else:
            template = l[0].strip()
            for char in l[0].strip():
                characters[char] = 0

for n in range(10):
    template = Insert(template, rules)

FindMaxMin(template, characters)