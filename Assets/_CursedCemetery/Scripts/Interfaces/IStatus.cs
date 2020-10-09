

namespace CursedCemetery.Scripts.Interfaces
{
	public interface IStatus  {

		void IncreaseLife(float life);

		void DecreaseLife(float life);

		void IncreaseArrows(float arrow);

		void DecreaseArrows(float arrow);

		float GetLife();

		float GetArrows();
	
	}
}
