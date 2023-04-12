class Elf
    @calories

    def initialize()
        @calories = 0
    end

    def cals 
        @calories
    end

    def addCalories(cal)          
        @calories = @calories + Integer(cal)
    end
end

class Elves
    @top3
    
    def initialize()
        @top3 = []
    end

    def addElf(el)
        @top3 << el
        worstElf = el
        if @top3.length() > 3
            @top3.each do |elf|
                if elf.cals < worstElf.cals
                    worstElf = elf
                end
                @top3.delete(worstElf)
            end

        end
    end

    def totalcals
        cals = 0
        @top3.each { |elf| cals = cals + elf.cals }
        puts(cals)
    end


end

elves = Elves.new
currentElf = Elf.new
File.foreach("input.in") do |line|
    if line.strip.empty?
        elves.addElf(currentElf)
        currentElf = Elf.new
    else
        currentElf.addCalories(line)
    end
end
elves.addElf(currentElf)
elves.totalcals