using System;
using Xunit;

namespace StreamDeckLib.Test
{

	public class ActionManagerShould
	{

		[Fact]
		public void ThrowActionNotRegisteredException_WhenRetrievingAnUnregisteredAction()
		{
			// Arrange
			using (var SUT = new ActionManager(null))
			{
				var cm = ConnectionManager.Initialize(StubProxy.ValidCommandLineArguments)
									.RegisterActionType("Unique_Action_ID_1", typeof(StubAction));
				// Act

				// Assert
				Assert.Throws<ActionNotRegisteredException>(() => SUT.GetAction(cm, "UUID1"));

			}
		}

		[Fact]
		public void ShowOneSingleActionRegistered_WhenRegisteringOnlyOneAction()
		{
			//
			// Arrange
			//

			var testAction = new StubAction();
			BaseStreamDeckAction returnedAction=null;

			// 
			// Act
			//
			using (var SUT = new ActionManager(null))
			{
				var cm = ConnectionManager.Initialize(StubProxy.ValidCommandLineArguments)
									.RegisterActionType("Unique_Action_ID_1", typeof(StubAction));

				SUT.RegisterAction<StubAction>("UUID1");
				returnedAction = SUT.GetActionInstance<StubAction>(cm, "UUID1");
			}

			//
			// Assert
			//
			Assert.NotNull(returnedAction);

			Assert.IsType<StubAction>(returnedAction);
		}



		[Fact]
		public void ShouldReturnTrue_WhenInquiredAboutRegistrationOfRegisteredActionUUID()
		{
			//
			// Arrange
			//
			using (var SUT = new ActionManager(null))
			{

				//
				// Act
				//
				SUT.RegisterAction<StubAction>("UUID1");

				//
				// Assert
				//
				Assert.True(SUT.IsActionRegistered("UUID1"));

			}
		}


		[Fact]
		public void ShouldReturnFalse_WhenInquiredAboutRegistrationOfUnregisteredActionUUID()
		{
			//
			// Arrange
			//
			using (var SUT = new ActionManager(null))
			{

				//
				// Act
				//
				SUT.RegisterAction<StubAction>("UUID1");

				//
				// Assert
				//
				Assert.False(SUT.IsActionRegistered("UUID2"));

			}
		}



		[Fact]
		public void ShouldReturnTrue_WhenInquiredAboutRegistrationOfActionUUIDWithDifferentCasing()
		{
			//
			// Arrange
			//
			using (var SUT = new ActionManager(null))
			{

				//
				// Act
				//
				SUT.RegisterAction<StubAction>("UUID1");

				//
				// Assert
				//
				Assert.True(SUT.IsActionRegistered("uuID1"));
			}
		}

	}
}
