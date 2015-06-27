MagicExpression
===============

Regular expression made simple in .Net


MagicExpression for muggles
---------------------------

```cs
var magicWand = Magex.New();

magicWand.Character('-').Repeat.AtMostOnce()
         .CharacterIn(Characters.Numeral).Repeat.Any()
         .Character('.')
         .CharacterIn(Characters.Numeral).Repeat.AtLeastOnce();

// Creates a regex corresponding to
// -?[0-9]*\.[0-9]+
var floatingPointNumberDetector = new Regex(magicWand.Expression);

// Will match "1.234", "-1.234", "0.0", ".01"
// Will not match "0" "1,234", "0x234", "#1a4f66"
```

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

Usage:

```cs
var magicWand = Magex.New();
var notSoMagicWand = Magex.New();

// You can use sub expressions, created before
// or create them in place
magicWand.Group(notSoMagicWand.Character('a'));
magicWand.Group(Magex.New().Character('a'));

// Or use lambdas
magicWand.Group(x => x.Character('a'));

magicWand = Magex.New();
// Reference a previous capture
// Will match something like <strong>hello world</strong> (but please don't parse HTML with Magex in real life)
magicWand.Character('<')
         .CaptureAs("tag", x => x.CharacterNotIn('>').Repeat.AtLeastOnce())
         .Character('>')
         .Character().Repeat.Any().Lazy()
         .String("</")
         .BackReference("tag")
         .Character('>');

var badHtmlTagDetector = new Regex(magicWand.Expression);

// Will match "<strong>hello world</strong>", "<h1>A title</h1>"
// Will not match "<h1>A tag mismatch</strong>"
 ```

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

```cs
var magicWand = Magex.New();

// With subexpressions
magicWand.Alternative(
    Magex.New().Character('a'),
    Magex.New().Character('b')
    );

var notSoMagicWand = Magex.New();
// With lambdas
notSoMagicWand.Alternative(
    x => x.Character('a'),
    x => x.Character('b')
    );

var alternativeDetector = new Regex(notSoMagicWand.Expression);

// Will match "a", "b"
// Will not match "c"
```

* `Alternative(params IExpressionElement[])` Matches the string if it corresponds to one alternative
* `Alternative(params Action<IMagex>[])` Same thing with 

### Laziness

```cs
// The group will match the smallest ensemble possible, e.g. "<em>" and "</em>"
var lazyMagicWand = Magex.New().Character('<')
         .Group(x => x.Character().Repeat.Any().Lazy())
         .Character('>');

// The group will match the larges ensemble possible, e.g. the whole "<em>something</em>"
var greedyMagicWand = Magex.New().Character('<')
         .Group(x => x.Character().Repeat.Any())
         .Character('>');
```
          
* `Lazy()` Makes the detection lazy instead of greedy (default)          
          
### Predefined Expressions

Usage:

```cs
var magicWand = Magex.New();

// Produces an expression of type (?<![1-9])([0-9]|[1-4][0-2])(?![0-9])
magicWand.Builder.NumericRange(0, 42);

var detector = new Regex(magicWand.Expression);

// Will match "0", "9", "20", "42"
// Will not match "43", "52"
```

* 'Literal(string)' inserts a regular expression

## License
Licensed under MIT License, Copyright 2014 Guillaume Gautreau, Timothée Bourguignon
