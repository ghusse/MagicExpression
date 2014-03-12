MagicExpression
===============

Regular expression made simple in .Net


MagicExpression for muggles
---------------------------

     var magicWand = Magex.CreateNew();
     
     magicWand.Character('-').Repeat.AtMostOnce()
              .CharacterIn(Character.Number).Repeat.Any()
              .Character('.')
              .CharacterIn(Character.Number).Repeat.AtLeastOnce();
     
     // Creates a regex corresponding to
     // -?[0-9]*\.[0-9]+
     Regex floatingPointNumberDetector = new Regex(magicWant.Expression);

### Add content to the MagicExpression

#### Characters

All of these functions are escaping special chars.

* `Character()` matches any character
* `Character(char)` matches the given character
* `CharacterIn(params char[])` matches any character in the list
* `CharacterIn(string)` matches any character in the list
* `CharacterIn(Characters, string)` matches any character in the list
* `CharacterIn(Characters params char[])` matches any character in the list
* `CharacterNotIn(params char[])` matches any character not in the list
* `CharacterNotIn(string)` matches any character not in the list
* `CharacterNotIn(Characters, string)` matches any character not in the list
* `CharacterNotIn(Characters params char[])` matches any character not in the list

`Characters` is an enumeration of flags, allowing you to add special characters to the list of matched characters.

#### Strings

* `String(string)` matches the given string, escaping special chars
     
### Repeat last element

Some content can be repeated. For this content, you can use the Repeat property and specify cardinality.

* `Repeat.Any()` will match any number of repetitions, including 0
* `Repeat.Once()` default behavior
* `Repeat.Times(uint)`
* `Repeat.Between(uint, uint)`
* `Repeat.AtLeastOnce()`
* `Repeat.AtMostOnce()`
    
### Groups, captures and backreferences

Usage
     
     var magicWand = Magex.CreateNew();
     
     // You can use sub expressions, created before
     // or create them in place
     magicWand.Group(Magex.CreateNew()
                      .Character('a'));
                      
    // Or use lambdas
    magicWand.Group(x => x.Character('a'));
    
    // Reference a previous capture
    // Will match something like <strong>hello world</strong> (don't parse HTML with magicEx in real life)
    magicWand.Character('<')
             .CaptureAs("tag", x => x.CharacterNotIn('>').Repeat.AtLeastOnce())
             .Character('>')
             .Character().Repeat.Any().Lazy()
             .String("</")
             .BackReference("tag")
             .Character('>');
    


* `Group(IExpressionElement)` Non capturing group
* `Group(Action<IMagex>)` Non capturing group defined with a lambda
* `Capture(IExpressionElement)` Indexed capture
* `Capture(Action<IMagex>)` Indexed capture defined with a lambda
* `CaptureAs(string, IExpressionElement)` Named capture
* `CaptureAs(string, Action<IMagex>)` Named capture defined with a lambda
* `BackReference(string)` references a named capture
* `BackReference(uint)` references an indexed capture

### Alternatives

Usage

     var magicWand = Magex.CreateNew();
     
     // With subexpressions
     magicWand.Alternative(
       Magex.CreateNew().Character('a'),
       Magex.CreateNew().Character('b')
     );
     
     // With lambdas
     magicWand.Alternative(
      x => x.Character('a'),
      x => x.Character('b')
     );

* `Alternative(params IExpressionElement[])` Matches the string if it corresponds to one alternative
* `Alternative(params Action<IMagex>[])` Same thing with 

### Laziness

    var magicWand = Magex.CreateNew();
    
    // Without the call to Lazy, the group will also match the character '>'
    magicWand.Character('<')
             Group(x => x.Character().Repeat.Any().Lazy())
             .Character('>');
          
* `Lazy()` Makes the detection lazy instead of greedy          
          


## License
Licensed under MIT License, Copyright 2014 Guillaume Gautreau, Timothée Bourguignon
