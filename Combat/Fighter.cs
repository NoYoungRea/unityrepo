using RPG.Core;
using RPG.Movement;
using UnityEngine;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Transform target;
        float timeSincLastAttack = 0;
        private void Update()
        {
            timeSincLastAttack += Time.deltaTime;

            if (target == null) return;
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSincLastAttack > timeBetweenAttacks)
            {

                GetComponent<Animator>().SetTrigger("attack");
                timeSincLastAttack = 0;


            }
        }
       
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TackDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;

        }
        public void Cancel()
        {
            target = null;
        }
    }
}
