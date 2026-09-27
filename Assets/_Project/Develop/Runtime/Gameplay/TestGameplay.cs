using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;


namespace Assets._Project.Develop.Runtime.Gameplay
{
    //L4 - Стартовая подготовка. Тестовый геймплей
    //L4 - Пользуемся фабрикой
    //L4 - Проверка системы и итог по MonoEntity
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;

        private Entity _entity;

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
        }

        //L4 - Тестируем работу с MonoEntity
        public void Run()
        {
            _entity = _entitiesFactory.CreateTestEntity(Vector3.zero);

            //Entity entity = _entitiesFactory.CreateTestEntity(Vector3.zero);


            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false) //если геймплей еще не запущен
                return;

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            //_entity.MoveDirection.Value = input;
            _entity.GetComponent<MoveDirection>().Value.Value = input;
        }
    }
}
