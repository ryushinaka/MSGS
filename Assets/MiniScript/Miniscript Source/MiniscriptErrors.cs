﻿/*	MiniScriptErrors.cs

This file defines the exception hierarchy used by MiniScript.
The core of the tree is this:

	MiniScriptException
		LexerException -- any error while finding tokens from raw source
		CompilerException -- any error while compiling tokens into bytecode
		RuntimeException -- any error while actually executing code.

We have a number of fine-grained exception types within these,
but they will always derive from one of those three (and ultimately
from MiniScriptException).
*/

using System;

namespace MiniScript {
	public class SourceLoc {
		public string context;	// file name, etc. (optional)
		public int lineNum;

		public SourceLoc(string context, int lineNum) {
			this.context = context;
			this.lineNum = lineNum;
		}

		public override string ToString() {
			return string.Format("[{0}line {1}]",
				string.IsNullOrEmpty(context) ? "" : context + " ",
				lineNum);
		}
	}

	public class MiniScriptException: Exception {
		public SourceLoc location;

		public MiniScriptException() {
		}

		public MiniScriptException(string message) : base(message) {
		}

		public MiniScriptException(string context, int lineNum, string message) : base(message) {
			location = new SourceLoc(context, lineNum);
		}

		public MiniScriptException(string message, Exception inner) : base(message, inner) {
		}

		/// <summary>
		/// Get a standard description of this error, including type and location.
		/// </summary>
		public string Description() {
			string desc = "Error: ";
			if (this is LexerException) desc = "Lexer Error: ";
			else if (this is CompilerException) desc = "Compiler Error: ";
			else if (this is RuntimeException) desc = "Runtime Error: ";
			desc += Message;
			if (location != null) desc += " " + location;
			return desc;		
		}

	}

	public class LexerException: MiniScriptException {
		public LexerException() : base("Lexer Error") {
		}

		public LexerException(string message) : base(message) {
		}

		public LexerException(string message, Exception inner) : base(message, inner) {
		}
	}

	public class CompilerException: MiniScriptException {
		public CompilerException() : base("Syntax Error") {
		}

		public CompilerException(string message) : base(message) {
		}

		public CompilerException(string context, int lineNum, string message) : base(context, lineNum, message) {
		}

		public CompilerException(string message, Exception inner) : base(message, inner) {
		}
	}

	public class RuntimeException: MiniScriptException {
		public RuntimeException() : base("Runtime Error") {
		}

		public RuntimeException(string message) : base(message) {
		}

		public RuntimeException(string message, Exception inner) : base(message, inner) {
		}
	}

	public class IndexException: RuntimeException {
		public IndexException() : base("Index Error (index out of range)") {
		}

		public IndexException(string message) : base(message) {
		}

		public IndexException(string message, Exception inner) : base(message, inner) {
		}
	}

	public class KeyException : RuntimeException {
		private KeyException() {}		// don't use this version

		public KeyException(string key) : base("Key Not Found: '" + key + "' not found in map") {
		}

		public KeyException(string message, Exception inner) : base(message, inner) {
		}
	}

	public class TypeException : RuntimeException {
		public TypeException() : base("Type Error (wrong type for whatever you're doing)") {
		}

		public TypeException(string message) : base(message) {
		}

		public TypeException(string message, Exception inner) : base(message, inner) {
		}
	}

	public class TooManyArgumentsException : RuntimeException {
		public TooManyArgumentsException() : base("Too Many Arguments") {
		}

		public TooManyArgumentsException(string message) : base(message) {
		}

		public TooManyArgumentsException(string message, Exception inner) : base(message, inner) {
		}
	}

	public class LimitExceededException : RuntimeException {
		public LimitExceededException() : base("Runtime Limit Exceeded") {
		}

		public LimitExceededException(string message) : base(message) {
		}

		public LimitExceededException(string message, Exception inner) : base(message, inner) {
		}
	}

	public class UndefinedIdentifierException : RuntimeException {
		private UndefinedIdentifierException() {}		// don't call this version!

		public UndefinedIdentifierException(string ident) : base(
			"Undefined Identifier: '" + ident + "' is unknown in this context") {
		}

		public UndefinedIdentifierException(string message, Exception inner) : base(message, inner) {
		}
	}

	public static class Check {
		public static void Range(int i, int min, int max, string desc="index") {
			if (i < min || i > max) {
				throw new IndexException(string.Format("Index Error: {0} ({1}) out of range ({2} to {3})",
					desc, i, min, max));
			}
		}
		
		public static void Type(Value val, System.Type requiredType, string desc=null) {
			if (!requiredType.IsInstanceOfType(val)) {
				throw new TypeException(string.Format("got a {0} where a {1} was required{2}",
					val.GetType(), requiredType, desc == null ? null : " (" + desc + ")"));
			}
		}
	}
}

