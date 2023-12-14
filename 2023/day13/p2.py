class Field:
    def __init__(self):
        self.rows = []

    def add_line(self, line):
        self.rows.append(list(line.strip()))

    def find_reflection(self, original):
        for i in range(len(self.rows[0]) - 1):
            row = self.rows[0]
            if(row[i] == row[i + 1] and self.confirm_reflection(i, self.rows) and (i + 1 != original)):
                return i + 1
            
        rotated = list(zip(*self.rows))[::-1]
        for i in range(len(rotated[0]) - 1):
            row = rotated[0]
            if(row[i] == row[i + 1] and self.confirm_reflection(i, rotated) and((i + 1) * 100 != original)):
                return (i + 1) * 100

        #self.print_me(self.rows)
        #self.print_me(rotated)
        return -1

    def find_original_reflection(self):
        for i in range(len(self.rows[0]) - 1):
            row = self.rows[0]
            if(row[i] == row[i + 1] and self.confirm_reflection(i, self.rows)):
                return i + 1
            
        rotated = list(zip(*self.rows))[::-1]
        for i in range(len(rotated[0]) - 1):
            row = rotated[0]
            if(row[i] == row[i + 1] and self.confirm_reflection(i, rotated)):
                return (i + 1) * 100

        #self.print_me(self.rows)
        #self.print_me(rotated)
        return -1
    
    def find_smudged_reflection(self):
        original = self.find_original_reflection()
        for row in self.rows:
            for i in range(len(row)):
                row[i] = '.' if row[i] == '#' else '#'
                val = self.find_reflection(original)
                row[i] = '.' if row[i] == '#' else '#'
                if val >= 0:
                    return val

    def print_me(self, rows):
        print(len(rows))
        for row in rows:
            line = ''.join(row)
            print(line, len(row))
        print(" ")

    def confirm_reflection(self, i, rows):
        for row in rows:
            n = 0 
            while i - n >= 0 and i + n + 1 < len(row):
                if(row[i - n] != row[i + n + 1]):
                    return False
                n += 1

        return True
    
fields = []
field = Field()
filename = "input.in"
with open(filename) as file:
    for line in file:
        # End of pattern or start of 
        if line.strip() == "":
            fields.append(field)
            field = Field()
        else: 
            field.add_line(line)
fields.append(field)
total = 0
for field in fields:
    total += field.find_smudged_reflection()
print(total)
        
